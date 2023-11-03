using IcarianEngine;
using IcarianEngine.Maths;
using IcarianEngine.Mod;
using IcarianEngine.Rendering;
using IcarianEngine.Rendering.Lighting;

namespace IcarianSponza
{
    public class IcarianSponzaAssemblyControl : AssemblyControl
    {
        Scene                m_scene = null;

        GameObject           m_camObject;

        DepthRenderTexture[] m_shadowMaps = new DepthRenderTexture[DefaultRenderPipeline.CascadeCount];

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
            lightObj.Transform.Rotation = Quaternion.FromAxisAngle(Vector3.Normalized(new Vector3(1.0f, 0.0f, 0.3f)), 1.25f);
            DirectionalLight light = lightObj.AddComponent<DirectionalLight>();
            light.Color = Color.FromColorCode(0xEEEEFFFF);
            light.Intensity = 0.5f;

            for (uint i = 0; i < DefaultRenderPipeline.CascadeCount; ++i)
            {
                m_shadowMaps[i] = new DepthRenderTexture(1024, 1024);

                light.AddShadowMap(m_shadowMaps[i]);
            }

            GameObject entranceLampObj = GameObject.Instantiate<GameObject>();
            entranceLampObj.Transform.Translation = new Vector3(-0.91866f, -3.8831f, -15.611f);
            entranceLampObj.AddComponent(PointLightDefTable.Sponza_LampPointLight);

            GameObject lamp00 = GameObject.Instantiate<GameObject>();
            lamp00.Transform.Translation = new Vector3(0.062637f, -4.56387f, 13.5982f);
            lamp00.AddComponent(PointLightDefTable.Sponza_LampPointLight);

            GameObject lamp01 = GameObject.Instantiate<GameObject>();
            lamp01.Transform.Translation = new Vector3(-4.79454f, -4.82332f, 9.82539f);
            lamp01.AddComponent(PointLightDefTable.Sponza_LampPointLight);

            GameObject lamp02 = GameObject.Instantiate<GameObject>();
            lamp02.Transform.Translation = new Vector3(-4.79454f, -4.82332f, 5.9221f);
            lamp02.AddComponent(PointLightDefTable.Sponza_LampPointLight);

            GameObject lamp03 = GameObject.Instantiate<GameObject>();
            lamp03.Transform.Translation = new Vector3(-4.79454f, -4.82332f, 1.8523f);
            lamp03.AddComponent(PointLightDefTable.Sponza_LampPointLight);

            GameObject lamp04 = GameObject.Instantiate<GameObject>();
            lamp04.Transform.Translation = new Vector3(-4.79454f, -4.82332f, -2.183f);
            lamp04.AddComponent(PointLightDefTable.Sponza_LampPointLight);

            GameObject lamp05 = GameObject.Instantiate<GameObject>();
            lamp05.Transform.Translation = new Vector3(-4.79454f, -4.82332f, -6.1465f);
            lamp05.AddComponent(PointLightDefTable.Sponza_LampPointLight);

            GameObject lamp06 = GameObject.Instantiate<GameObject>();
            lamp06.Transform.Translation = new Vector3(-4.79454f, -4.82332f, -10.027f);
            lamp06.AddComponent(PointLightDefTable.Sponza_LampPointLight);

            GameObject lamp07 = GameObject.Instantiate<GameObject>();
            lamp07.Transform.Translation = new Vector3(4.74035f, -4.82332f, 9.82539f);
            lamp07.AddComponent(PointLightDefTable.Sponza_LampPointLight);

            GameObject lamp08 = GameObject.Instantiate<GameObject>();
            lamp08.Transform.Translation = new Vector3(4.74035f, -4.82332f, 5.9221f);
            lamp08.AddComponent(PointLightDefTable.Sponza_LampPointLight);

            GameObject lamp09 = GameObject.Instantiate<GameObject>();
            lamp09.Transform.Translation = new Vector3(4.74035f, -4.82332f, 1.8523f);
            lamp09.AddComponent(PointLightDefTable.Sponza_LampPointLight);

            GameObject lamp10 = GameObject.Instantiate<GameObject>();
            lamp10.Transform.Translation = new Vector3(4.74035f, -4.82332f, -2.183f);
            lamp10.AddComponent(PointLightDefTable.Sponza_LampPointLight);

            GameObject lamp11 = GameObject.Instantiate<GameObject>();
            lamp11.Transform.Translation = new Vector3(4.74035f, -4.82332f, -6.1465f);
            lamp11.AddComponent(PointLightDefTable.Sponza_LampPointLight);

            GameObject lamp12 = GameObject.Instantiate<GameObject>();
            lamp12.Transform.Translation = new Vector3(4.74035f, -4.82332f, -10.027f);
            lamp12.AddComponent(PointLightDefTable.Sponza_LampPointLight);
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
        public override void FixedUpdate()
        {
            // Assembly FixedUpdate
        }

        public override void Close()
        {
            // Assembly Shutdown
            Logger.Message("Sponza Shutdown");

            for (uint i = 0; i < DefaultRenderPipeline.CascadeCount; ++i)
            {
                m_shadowMaps[i].Dispose();
            }

            if (m_scene != null)
            {
                m_scene.Dispose();
            }
        }
    }
}
