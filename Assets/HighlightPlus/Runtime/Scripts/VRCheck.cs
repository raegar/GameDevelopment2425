// Comment this line to disable usage of XR module
#define ENABLE_XR

using System.Collections.Generic;
using UnityEngine;

#if ENABLE_XR
using UnityEngine.XR;
#endif

namespace HighlightPlus {

    static class VRCheck {

        public static bool isActive;
        public static bool isVrRunning;

#if !ENABLE_XR
            static bool IsActive() {
                return false;
            }

            static bool IsVrRunning() {
                return false;
            }

#else

        static readonly List<XRDisplaySubsystemDescriptor> displaysDescs = new List<XRDisplaySubsystemDescriptor>();
        static readonly List<XRDisplaySubsystem> displays = new List<XRDisplaySubsystem>();

        static bool IsActive() {
            displaysDescs.Clear();
            SubsystemManager.GetSubsystemDescriptors(displaysDescs);

            // If there are registered display descriptors that is a good indication that VR is most likely "enabled"
            return displaysDescs.Count > 0;
        }

        static bool IsVrRunning() {
            bool vrIsRunning = false;
            displays.Clear();
            SubsystemManager.GetInstances(displays);
            foreach (var displaySubsystem in displays) {
                if (displaySubsystem.running) {
                    vrIsRunning = true;
                    break;
                }
            }

            return vrIsRunning;
        }

#endif

        public static void Init() {
            isActive = IsActive();
            isVrRunning = IsVrRunning();
        }

    }
}