// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

extern alias MicrosoftXnaFramework;
using Microsoft.Xna.Framework.Input.Touch;
using MsXna_FrameworkDispatcher = MicrosoftXnaFramework::Microsoft.Xna.Framework.FrameworkDispatcher; 

using System;
using System.Diagnostics;
using System.Windows.Controls;
using Microsoft.Xna.Framework;
using Windows.ApplicationModel.Activation;

namespace MonoGame.Framework.WindowsPhone
{
    class WindowsPhoneGamePlatform : GamePlatform
    {
        internal static string LaunchParameters;

        internal static ApplicationExecutionState PreviousExecutionState { get; set; }

        public WindowsPhoneGamePlatform(Game game)
            : base(game)
        {
            // Setup the game window.
            Window = new WindowsPhoneGameWindow(game);

            // Setup the launch parameters.
            // - Parameters can optionally start with a forward slash.
            // - Keys can be separated from values by a colon or equals sign
            // - Double quotes can be used to enclose spaces in a key or value.
            int pos = 0;
            int paramStart = 0;
            bool inQuotes = false;
            var keySeperators = new char[] { ':', '=' };

            while (pos <= LaunchParameters.Length)
            {
                string arg = string.Empty;
                if (pos < LaunchParameters.Length)
                {
                    char c = LaunchParameters[pos];
                    if (c == '"')
                        inQuotes = !inQuotes;
                    else if ((c == ' ') && !inQuotes)
                    {
                        arg = LaunchParameters.Substring(paramStart, pos - paramStart).Replace("\"", "");
                        paramStart = pos + 1;
                    }
                }
                else
                {
                    arg = LaunchParameters.Substring(paramStart).Replace("\"", "");
                }
                ++pos;

                if (string.IsNullOrWhiteSpace(arg))
                    continue;

                string key = string.Empty;
                string value = string.Empty;
                int keyStart = 0;

                if (arg.StartsWith("/"))
                    keyStart = 1;

                if (arg.Length > keyStart)
                {
                    int keyEnd = arg.IndexOfAny(keySeperators, keyStart);

                    if (keyEnd >= 0)
                    {
                        key = arg.Substring(keyStart, keyEnd - keyStart);
                        int valueStart = keyEnd + 1;
                        if (valueStart < arg.Length)
                            value = arg.Substring(valueStart);
                    }
                    else
                    {
                        key = arg.Substring(keyStart);
                    }

                    Game.LaunchParameters.Add(key, value);
                }
            }

            Game.PreviousExecutionState = PreviousExecutionState;
        }

        public override GameRunBehavior DefaultRunBehavior
        {
            get { return GameRunBehavior.Asynchronous; }
        }

        public override void RunLoop()
        {
            throw new NotSupportedException("The Windows Phone platform does not support synchronous run loops");
        }

        public override void StartRunLoop()
        {
        }
        
        public override void Exit()
        {
            // Closing event is not fired when termiate is called. We need to deactivate the game manually.
            if (Game.Instance != null)
                this.IsActive = false;

            System.Windows.Application.Current.Terminate();
        }

        public override bool BeforeUpdate(GameTime gameTime)
        {
            MsXna_FrameworkDispatcher.Update();
            Window.TouchPanelState.ProcessQueued();
            return true;
        }

        public override bool BeforeDraw(GameTime gameTime)
        {
            return true;
        }

        public override void EnterFullScreen()
        {
        }

        public override void ExitFullScreen()
        {
        }

        public override void EndScreenDeviceChange(string screenDeviceName, int clientWidth, int clientHeight)
        {
            Window.EndScreenDeviceChange(screenDeviceName, clientWidth, clientHeight);
        }

        public override void BeginScreenDeviceChange(bool willBeFullScreen)
        {
            Window.BeginScreenDeviceChange(willBeFullScreen);
        }

        public override void Log(string message)
        {
            Debug.WriteLine(message);
        }

        public override void Present()
        {
        }

        protected override void OnIsMouseVisibleChanged() 
        {
        }
		
        protected override void Dispose(bool disposing)
        {
            // Make sure we dispose the graphics system.
            var graphicsDeviceManager = Game.graphicsDeviceManager;
            if (graphicsDeviceManager != null)
                graphicsDeviceManager.Dispose();

            //MetroGameWindow.Instance.Dispose();
	
			base.Dispose(disposing);
        }
    }
}
