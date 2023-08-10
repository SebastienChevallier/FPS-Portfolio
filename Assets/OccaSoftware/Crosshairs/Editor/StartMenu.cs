using UnityEngine;
using UnityEditor;

namespace OccaSoftware.Crosshairs.Editor
{
    public class StartMenu : EditorWindow
    {
        // Source for UUID: https://shortunique.id/
        private static string _modalId = "ShowCrosshairsModal=qtN0wW";
        private Texture2D _logo;
        private GUIStyle _header,
            _button,
            _contentSection;
        private GUILayoutOption[] _contentLayoutOptions;
        private static bool _listenToEditorUpdates;
        private static StartMenu _startMenu;

        [MenuItem("OccaSoftware/Start Menu (Crosshairs)")]
        public static void SetupMenu()
        {
            _startMenu = CreateWindow();
            CenterWindowInEditor(_startMenu);
            LoadLogo(_startMenu);
        }

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            RegisterModal();
        }

        void OnGUI()
        {
            SetupHeaderStyle(_startMenu);
            SetupButtonStyle(_startMenu);
            SetupContentSectionStyle(_startMenu);

            DrawHeader();
            DrawReviewRequest();
            DrawHelpLinks();
            DrawUpgradeLinks();
        }

        #region Setup
        private static StartMenu CreateWindow()
        {
            StartMenu startMenu = (StartMenu)GetWindow(typeof(StartMenu));
            startMenu.position = new Rect(0, 0, 270, 480);
            return startMenu;
        }

        private static void CenterWindowInEditor(EditorWindow startMenu)
        {
            Rect mainWindow = EditorGUIUtility.GetMainWindowPosition();
            Rect currentWindowPosition = startMenu.position;
            float centerX = (mainWindow.width - currentWindowPosition.width) * 0.5f;
            float centerY = (mainWindow.height - currentWindowPosition.height) * 0.5f;
            currentWindowPosition.x = mainWindow.x + centerX;
            currentWindowPosition.y = mainWindow.y + centerY;
            startMenu.position = currentWindowPosition;
        }

        private static void LoadLogo(StartMenu startMenu)
        {
            startMenu._logo = (Texture2D)
                AssetDatabase.LoadAssetAtPath(
                    "Assets/OccaSoftware/Crosshairs/Editor/Textures/Logo.png",
                    typeof(Texture2D)
                );
        }

        private static void SetupHeaderStyle(StartMenu startMenu)
        {
            startMenu._header = new GUIStyle(EditorStyles.boldLabel);
            startMenu._header.fontSize = 18;
            startMenu._header.wordWrap = true;
            startMenu._header.padding = new RectOffset(0, 0, 0, 0);
        }

        private static void SetupButtonStyle(StartMenu startMenu)
        {
            startMenu._button = new GUIStyle("button");
            startMenu._button.fontSize = 18;
            startMenu._button.fontStyle = FontStyle.Bold;
            startMenu._button.fixedHeight = 40;
        }

        private static void SetupContentSectionStyle(StartMenu startMenu)
        {
            startMenu._contentSection = new GUIStyle("label");
            startMenu._contentSection.margin = new RectOffset(20, 20, 20, 20);
            startMenu._contentSection.padding = new RectOffset(0, 0, 0, 0);
            startMenu._contentLayoutOptions = new GUILayoutOption[] { GUILayout.MinWidth(230) };
        }
        #endregion


        #region Modal Handler
        private static void RegisterModal()
        {
            if (!_listenToEditorUpdates && !EditorApplication.isPlayingOrWillChangePlaymode)
            {
                _listenToEditorUpdates = true;
                EditorApplication.update += PopModal;
            }
        }

        private static void PopModal()
        {
            EditorApplication.update -= PopModal;

            bool showModal = EditorPrefs.GetBool(_modalId, true);
            if (showModal)
            {
                EditorPrefs.SetBool(_modalId, false);
                SetupMenu();
            }
        }
        #endregion



        #region UI Drawer
        private void DrawHeader()
        {
            GUILayout.BeginVertical(_contentSection, _contentLayoutOptions);
            GUIStyle logoStyle = new GUIStyle("label");
            GUILayoutOption[] logoOptions = new GUILayoutOption[] { GUILayout.Width(230) };
            logoStyle.padding = new RectOffset(0, 0, 0, 0);
            logoStyle.margin = new RectOffset(0, 0, 0, 0);
            logoStyle.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label(_logo, logoStyle, logoOptions);
            GUILayout.EndVertical();
        }

        private void DrawReviewRequest()
        {
            GUILayout.BeginVertical(_contentSection, _contentLayoutOptions);
            GUILayout.Label("What do you think about my free Crosshair pack?", _header);

            if (
                GUILayout.Button(
                    "Leave a review",
                    _button,
                    new GUILayoutOption[] { GUILayout.MaxWidth(300) }
                )
            )
            {
                Application.OpenURL("https://assetstore.unity.com/packages/slug/216732");
            }
            GUILayout.EndVertical();
        }

        private void DrawHelpLinks()
        {
            GUILayout.BeginVertical(_contentSection, _contentLayoutOptions);
            GUILayout.Label("I am here to help.", _header);
            if (EditorGUILayout.LinkButton("Website"))
            {
                Application.OpenURL("https://www.occasoftware.com/assets/crosshairs");
            }

            if (EditorGUILayout.LinkButton("Discord"))
            {
                Application.OpenURL("https://www.occasoftware.com/discord");
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawUpgradeLinks()
        {
            GUILayout.BeginVertical(_contentSection, _contentLayoutOptions);

            GUILayout.Label("Make your game a success.", _header);
            if (EditorGUILayout.LinkButton("Upgrade to Crosshairs Pro"))
            {
                Application.OpenURL("https://assetstore.unity.com/packages/slug/239049");
            }
            if (EditorGUILayout.LinkButton("Join my Newsletter"))
            {
                Application.OpenURL("https://www.occasoftware.com/newsletter");
            }
            EditorGUILayout.EndVertical();
        }
        #endregion
    }
}
