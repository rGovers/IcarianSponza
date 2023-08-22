using IcarianEngine;
using IcarianEngine.Maths;
using IcarianEngine.Mod;
using IcarianEngine.Rendering;
using IcarianEngine.Rendering.Lighting;

namespace IcarianSponza
{
    public class IcarianSponzaAssemblyControl : AssemblyControl
    {
        Scene m_scene = null;

        GameObject m_camObject;

        void LoadMainScene(Scene a_scene, LoadStatus a_status)
        {
            if (a_status == LoadStatus.Loaded)
            {
                Logger.Message("Sponza Scene Loaded");
                
                m_scene = a_scene;
                
                m_scene.GenerateScene(Matrix4.Identity);
            }
            else
            {
                Logger.Error("Sponza Scene Failed to Load");
            }
        }

        public override void Init()
        {
            // Assembly Initialization
            Logger.Message("Sponza Initialization");

            if (!Application.IsHeadless)
            {
                Monitor[] monitors = Application.GetMonitors();

                Application.SetFullscreen(monitors[0], true, monitors[0].Width, monitors[0].Height);
            }

            Scene.LoadSceneAsync("Sponza.iscene", LoadMainScene, JobPriority.High);

            m_camObject = GameObject.Instantiate<GameObject>();
            m_camObject.AddComponent<Camera>();

            GameObject lightObj = GameObject.Instantiate<GameObject>();
            lightObj.Transform.Rotation = Quaternion.FromAxisAngle(Vector3.Normalized(new Vector3(1.0f, 0.0f, 0.5f)), 2.0f);
            DirectionalLight light = lightObj.AddComponent<DirectionalLight>();
            light.Color = Color.FromColorCode(0xEEEEFFFF);
            light.Intensity = 2.5f;
        }

        public override void Update()
        {
            // Assembly Update
            m_camObject.Transform.Translation = new Vector3(0.0f, -2.5f + Mathf.Sin(Time.TimePassed) * 0.01f, 0.0f);
            m_camObject.Transform.Rotation = Quaternion.FromAxisAngle(Vector3.Up, Time.TimePassed * 0.05f);

            if (Input.IsKeyDown(KeyCode.Escape))
            {
                Application.Close();
            }
        }

        public override void Close()
        {
            // Assembly Shutdown
            Logger.Message("Sponza Shutdown");

            if (m_scene != null)
            {
                m_scene.Dispose();
            }
        }
    }
}
