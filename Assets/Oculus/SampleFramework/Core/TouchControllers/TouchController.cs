/************************************************************************************

Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.  

See SampleFramework license.txt for license terms.  Unless required by applicable law 
or agreed to in writing, the sample code is provided �AS IS� WITHOUT WARRANTIES OR 
CONDITIONS OF ANY KIND, either express or implied.  See the license for specific 
language governing permissions and limitations under the license.

************************************************************************************/

using UnityEngine;
using System.Collections;

namespace OVRTouchSample
{
    // Animating controller that updates with the tracked controller.
    public class TouchController : MonoBehaviour
    {
        [SerializeField]
        private OVRInput.Controller m_controller = OVRInput.Controller.None;
        [SerializeField]
        private Animator m_animator = null;

        private bool m_restoreOnInputAcquired = false;

        public bool animating=false;

        public Material normal;
        public Material active;

        public Renderer thumbstickRenderer;
        public Renderer secondaryButtonRenderer;
        public Renderer rightTriggerRenderer;


        public bool cameraRotationActive = false;

        private void Start() {
            thumbstickRenderer.material = normal;
        }

        private void Update()
        {
            m_animator.SetFloat("Button 1", OVRInput.Get(OVRInput.Button.One, m_controller) ? 1.0f : 0.0f);
            m_animator.SetFloat("Button 2", OVRInput.Get(OVRInput.Button.Two, m_controller) ? 1.0f : 0.0f);
            if(!animating)
            {
                m_animator.SetFloat("Joy X", OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controller).x);
            }
            m_animator.SetFloat("Joy Y", OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controller).y);
            m_animator.SetFloat("Grip", OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller));
            m_animator.SetFloat("Trigger", OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, m_controller));

            OVRManager.InputFocusAcquired += OnInputFocusAcquired;
            OVRManager.InputFocusLost += OnInputFocusLost;
        }

        public void SetCameraRotationActive()
        {
            cameraRotationActive=true;
        }

        public void ActiveThumstickController()
        {
            thumbstickRenderer.material = active;
            StartCoroutine("ActiveAnimationRotateCamera");
        }

        public void DisableThumstickController()
        {
            thumbstickRenderer.material = normal;
            animating = false;
        }

        public void SetSecondaryButtonState(string state)
        {
            if(state=="active")
            {
                secondaryButtonRenderer.material = active;
            }
            else
            {
                secondaryButtonRenderer.material = normal;
            }
        }

        public void SetTriggerState(string state)
        {
            if(state=="active")
            {
                rightTriggerRenderer.material = active;
            }
            else
            {
                rightTriggerRenderer.material = normal;
            }
        }


        public IEnumerator ActiveAnimationRotateCamera()
        {
            animating = true;
            yield return new WaitForSeconds(2f);
            m_animator.SetFloat("Joy X", 100f);
            yield return new WaitForSeconds(1f);
            m_animator.SetFloat("Joy X", 0f);
            yield return new WaitForSeconds(1f);
            m_animator.SetFloat("Joy X", -100f);
            yield return new WaitForSeconds(1f);
            m_animator.SetFloat("Joy X", -0f);
            yield return new WaitForSeconds(1f);
            m_animator.SetFloat("Joy X", 100f);
            yield return new WaitForSeconds(1f);
            m_animator.SetFloat("Joy X", 0f);
            yield return new WaitForSeconds(1f);
            m_animator.SetFloat("Joy X", -100f);
            yield return new WaitForSeconds(1f);
            m_animator.SetFloat("Joy X", -0f);
            yield return new WaitForSeconds(1f);
            animating = false;
        }

        private void OnInputFocusLost()
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
                m_restoreOnInputAcquired = true;
            }
        }

        private void OnInputFocusAcquired()
        {
            if (m_restoreOnInputAcquired)
            {
                gameObject.SetActive(true);
                m_restoreOnInputAcquired = false;
            }
        }

    }
}
