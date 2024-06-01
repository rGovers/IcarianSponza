using IcarianEngine;
using IcarianEngine.Maths;
using IcarianEngine.Mod;
using IcarianEngine.Rendering;
using IcarianEngine.Rendering.PostEffects;
using IcarianEngine.Rendering.Lighting;
using System.Collections.Generic;

namespace IcarianSponza
{
    public class IcarianSponzaAssemblyControl : AssemblyControl
    {
        Scene                        m_scene = null;
         
        GameObject                   m_camObject;

        DepthRenderTexture[]         m_shadowMaps = new DepthRenderTexture[DefaultRenderPipeline.CascadeCount];
        List<DepthCubeRenderTexture> m_shadowCubeMaps = new List<DepthCubeRenderTexture>();

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
            DefaultRenderPipeline pipeline = new DefaultRenderPipeline(new PostEffect[] 
            {
                new EmissionPostEffect(),
                new ToneMapPostEffect()
            });
            
            pipeline.SetRenderScale(2.0f);

            RenderPipeline.SetPipeline(pipeline);

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
            AmbientLight ambientLight = lightObj.AddComponent<AmbientLight>();
            ambientLight.Color = Color.FromColorCode(0xDDDDEEFF);
            ambientLight.Intensity = 0.1f;
            DirectionalLight light = lightObj.AddComponent<DirectionalLight>();
            light.Color = Color.FromColorCode(0xEEEEFFFF);
            light.Intensity = 0.5f;

            for (uint i = 0; i < DefaultRenderPipeline.CascadeCount; ++i)
            {
                m_shadowMaps[i] = new DepthRenderTexture(2048, 2048);

                light.AddShadowMap(m_shadowMaps[i]);
            }
            
            for (int i = 0; i < 14; ++i)
            {
                m_shadowCubeMaps.Add(new DepthCubeRenderTexture(1024, 1024));
            }

            GameObject entranceLampObj = GameObject.Instantiate<GameObject>();
            entranceLampObj.Transform.Translation = new Vector3(-0.91866f, -3.8831f, -15.611f);
            PointLight entrancePointLight = entranceLampObj.AddComponent<PointLight>(PointLightDefTable.Sponza_LampPointLight);
            entrancePointLight.ShadowMap = m_shadowCubeMaps[0];

            GameObject lamp00 = GameObject.Instantiate<GameObject>();
            lamp00.Transform.Translation = new Vector3(0.062637f, -3.93195f, 13.5982f);
            PointLight lamp00PointLight = lamp00.AddComponent<PointLight>(PointLightDefTable.Sponza_LampPointLight);
            lamp00PointLight.ShadowMap = m_shadowCubeMaps[1];

            GameObject lamp01 = GameObject.Instantiate<GameObject>();
            lamp01.Transform.Translation = new Vector3(-4.79454f, -4.1914f, 9.82539f);
            PointLight lamp01PointLight = lamp01.AddComponent<PointLight>(PointLightDefTable.Sponza_LampPointLight);
            lamp01PointLight.ShadowMap = m_shadowCubeMaps[2];

            GameObject lamp02 = GameObject.Instantiate<GameObject>();
            lamp02.Transform.Translation = new Vector3(-4.79454f, -4.1914f, 5.9221f);
            PointLight lamp02PointLight = lamp02.AddComponent<PointLight>(PointLightDefTable.Sponza_LampPointLight);
            lamp02PointLight.ShadowMap = m_shadowCubeMaps[3];

            GameObject lamp03 = GameObject.Instantiate<GameObject>();
            lamp03.Transform.Translation = new Vector3(-4.79454f, -4.1914f, 1.8523f);
            PointLight lamp03PointLight = lamp03.AddComponent<PointLight>(PointLightDefTable.Sponza_LampPointLight);
            lamp03PointLight.ShadowMap = m_shadowCubeMaps[4];

            GameObject lamp04 = GameObject.Instantiate<GameObject>();
            lamp04.Transform.Translation = new Vector3(-4.79454f, -4.1914f, -2.183f);
            PointLight lamp04PointLight = lamp04.AddComponent<PointLight>(PointLightDefTable.Sponza_LampPointLight);
            lamp04PointLight.ShadowMap = m_shadowCubeMaps[5];

            GameObject lamp05 = GameObject.Instantiate<GameObject>();
            lamp05.Transform.Translation = new Vector3(-4.79454f, -4.1914f, -6.1465f);
            PointLight lamp05PointLight = lamp05.AddComponent<PointLight>(PointLightDefTable.Sponza_LampPointLight);
            lamp05PointLight.ShadowMap = m_shadowCubeMaps[6];

            GameObject lamp06 = GameObject.Instantiate<GameObject>();
            lamp06.Transform.Translation = new Vector3(-4.79454f, -4.1914f, -10.027f);
            PointLight lamp06PointLight = lamp06.AddComponent<PointLight>(PointLightDefTable.Sponza_LampPointLight);
            lamp06PointLight.ShadowMap = m_shadowCubeMaps[7];

            GameObject lamp07 = GameObject.Instantiate<GameObject>();
            lamp07.Transform.Translation = new Vector3(4.74035f, -4.1914f, 9.82539f);
            PointLight lamp07PointLight = lamp07.AddComponent<PointLight>(PointLightDefTable.Sponza_LampPointLight);
            lamp07PointLight.ShadowMap = m_shadowCubeMaps[8];

            GameObject lamp08 = GameObject.Instantiate<GameObject>();
            lamp08.Transform.Translation = new Vector3(4.74035f, -4.1914f, 5.9221f);
            PointLight lamp08PointLight = lamp08.AddComponent<PointLight>(PointLightDefTable.Sponza_LampPointLight);
            lamp08PointLight.ShadowMap = m_shadowCubeMaps[9];

            GameObject lamp09 = GameObject.Instantiate<GameObject>();
            lamp09.Transform.Translation = new Vector3(4.74035f, -4.1914f, 1.8523f);
            PointLight lamp09PointLight = lamp09.AddComponent<PointLight>(PointLightDefTable.Sponza_LampPointLight);
            lamp09PointLight.ShadowMap = m_shadowCubeMaps[10];

            GameObject lamp10 = GameObject.Instantiate<GameObject>();
            lamp10.Transform.Translation = new Vector3(4.74035f, -4.1914f, -2.183f);
            PointLight lamp10PointLight = lamp10.AddComponent<PointLight>(PointLightDefTable.Sponza_LampPointLight);
            lamp10PointLight.ShadowMap = m_shadowCubeMaps[11];

            GameObject lamp11 = GameObject.Instantiate<GameObject>();
            lamp11.Transform.Translation = new Vector3(4.74035f, -4.1914f, -6.1465f);
            PointLight lamp11PointLight = lamp11.AddComponent<PointLight>(PointLightDefTable.Sponza_LampPointLight);
            lamp11PointLight.ShadowMap = m_shadowCubeMaps[12];

            GameObject lamp12 = GameObject.Instantiate<GameObject>();
            lamp12.Transform.Translation = new Vector3(4.74035f, -4.1914f, -10.027f);
            PointLight lamp12PointLight = lamp12.AddComponent<PointLight>(PointLightDefTable.Sponza_LampPointLight);
            lamp12PointLight.ShadowMap = m_shadowCubeMaps[13];
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

            for (int i = 0; i < m_shadowCubeMaps.Count; ++i)
            {
                m_shadowCubeMaps[i].Dispose();
            }
            m_shadowCubeMaps.Clear();

            if (m_scene != null)
            {
                m_scene.Dispose();
            }
        }
    }
}
