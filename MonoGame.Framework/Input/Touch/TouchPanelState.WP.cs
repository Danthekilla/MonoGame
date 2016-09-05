// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Input.Touch
{
    public partial class TouchPanelState
    {
        private struct TouchEvent
        {
            public readonly int Id;
            public readonly TouchLocationState State;
            public readonly Vector2 Position;
            public readonly bool IsMouse;

            public TouchEvent(int id, TouchLocationState state, ref Vector2 position, bool isMouse)
            {
                Id = id;
                State = state;
                Position = position;
                IsMouse = isMouse;
            }
        }
        
        private readonly Queue<TouchEvent> _queue = new Queue<TouchEvent>();
        

        private void PlatformAddEvent(int id, TouchLocationState state, Vector2 position, bool isMouse)
        {
    	    /// Stores touches to apply them once a frame for platforms that dispatch touches asynchronously
    	    /// while user code is running.
            lock (_queue)
            {
                _queue.Enqueue(new TouchEvent(id, state, ref position, isMouse));
            }
        }

        private void PlatformProcessQueued()
        {
            lock (_queue)
            {
                while (_queue.Count > 0)
                {
                    var ev = _queue.Dequeue();
                    AddEventInternal(ev.Id, ev.State, ev.Position, ev.IsMouse);
                }
            }
        }
    }
}
