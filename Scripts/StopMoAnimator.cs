using UnityEngine;
using System.Collections;

namespace GPGre.StopMo{
	
	public enum StopMoAnimationStyle
	{
	    Once,
	    Loop,
	    PingPong
	}

	/// <summary>
	/// Read and play StopMo Animation Clip using this component
	/// </summary>

	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class StopMoAnimator : MonoBehaviour {

	    public StopMoAnimationClip stopMoAnimationClip;
	    public bool playOnAwake = false;

	    public int currentFrame { get; private set; }
	    public bool isPlaying { get; private set; }
	    public StopMoAnimationStyle playMode = StopMoAnimationStyle.Loop;

	    private MeshFilter meshFilter { get { return GetComponent<MeshFilter>(); } }
	    private MeshRenderer meshRenderer { get { return GetComponent<MeshRenderer>(); } }


	    public void Start()
	    {
	        currentFrame = 0;
	        isPlaying = false;
	        if(!IsEmpty())
	        {
				SwitchMesh(stopMoAnimationClip.frames[currentFrame]);
	        }
	        if(playOnAwake)
	            Play();
	    }

	    public void Play()
	    {
	        if (IsEmpty())
	        {
	            Debug.LogError("Can't play StopMoMesh : StopMoMesh missing or empty");
	            return;  
	        }
	        if (!isPlaying)
	        {
	            StartCoroutine("StopMoRoutine");
	        }

	    }

	    public void Pause()
	    {
	        if (IsEmpty())
	        {
	            Debug.LogError("Can't pause StopMoMesh : StopMoMesh missing or empty");
	            return;
	        }

	        if (isPlaying)
	        {
	            StopCoroutine("StopMoRoutine");
	            isPlaying = false;
	        }
	    }

	    public void Stop()
	    {
	        Pause();
	        currentFrame = 0;
	    }

	    IEnumerator StopMoRoutine()
	    {
	        float timeElapsed = 0;
	        int coeff = 1;

	        isPlaying = true;
	        while(true)
	        {
	            if(timeElapsed == 0)
	            {
					SwitchMesh(stopMoAnimationClip.frames[currentFrame]);
	            }
	            timeElapsed += Time.deltaTime;

				if (timeElapsed * stopMoAnimationClip.frameRate > 1)
	            {
	                currentFrame += coeff;

					if(currentFrame > stopMoAnimationClip.frames.Length -1 || currentFrame < 0)
	                {
	                    if (playMode == StopMoAnimationStyle.Once)
	                        break;
	                    else if (playMode == StopMoAnimationStyle.Loop)
	                        currentFrame = 0;
	                    else
	                    {
	                        coeff *= -1;
							currentFrame += stopMoAnimationClip.frames.Length > 1 ? coeff * 2 : coeff;
	                    }
	                }
	                timeElapsed = 0;
	            }

	            yield return 0;
	        }
	            isPlaying = false;
	    }

	    public bool IsEmpty()
	    {
			return stopMoAnimationClip == null || stopMoAnimationClip.frames.Length == 0;
	    }

	    public void SwitchMesh(MeshRenderer frame)
	    {
	        meshRenderer.material = frame.sharedMaterial;
	        meshFilter.mesh = frame.GetComponent<MeshFilter>().sharedMesh;
	    }


	    public void Triggered()
	    {
	        Stop();
	        playMode = StopMoAnimationStyle.Once;
	        Play();
	    }
	}
}