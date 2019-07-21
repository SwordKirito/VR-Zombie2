using System.Collections;

using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    [Tooltip("怪物血量")]
    [SerializeField]
    private float healthPoint = 5f;


    [Tooltip("伤害值")]
    [SerializeField]
    private float damage = 1f;


    [Tooltip("攻击音效")]
    [SerializeField]
    private AudioClip attackAC;



    [Tooltip("死亡音效")]
    [SerializeField]
    private AudioClip deadAC;


    [Tooltip("平常音效")]
    [SerializeField]
    private AudioClip walkAC;

    private NavMeshAgent agent;
    private GameObject player;
    private Animator ani;
    void OnEnable()

    {
        agent = this.GetComponent<NavMeshAgent>();
        if(agent == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
            agent.radius = 0.3f;
        }
        agent.enabled = true;

    }

    


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        ani = this.GetComponent<Animator>();
	}
	

    void PlaySound(AudioClip ac)
    {
        AudioSource Ac = this.GetComponent<AudioSource>();
        if(Ac == null)
        {
            Ac = this.gameObject.AddComponent<AudioSource>();
        }
        Ac.clip = ac;
        Ac.Play();
    }

    void ApplyDamage(float _damageAmount)
    {
        float t = healthPoint - _damageAmount;
        if (t > 0)
        {
            healthPoint = t;

        }
        else
        {
            healthPoint = 0;
            SwitchDead();
            HitHapticPulse(1000);
        }
    }

    void HitHapticPulse(ushort duration)
    {
        var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        var deviceIndex1 = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
        SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse(duration);
        SteamVR_Controller.Input(deviceIndex1).TriggerHapticPulse(duration);

    }




    void SwitchDead()
    {
        ani.SetBool("isDead", true);
        agent.enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        PlaySound(deadAC);
    }

    void SwitchAttack(bool value)
    {
        ani.SetBool("isAttack", value);
        PlaySound(attackAC);
    }





	// Update is called once per frame
	void Update ()
    {
		if(healthPoint <= 0)
        {
            AnimatorStateInfo asi = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if(asi.normalizedTime>1f && asi.IsName("back_fall")){
                Lean.LeanPool.Despawn(this.gameObject);
            }
        }
        if(agent.enabled && healthPoint > 0)
        {
            agent.destination = player.transform.position;
            if (Vector3.Distance(this.transform.position, player.transform.position) < 2.5f)
            {
                SwitchAttack(true);
                agent.Stop();
            }
            else
            {
                SwitchAttack(false);
                PlaySound(walkAC);
                agent.Resume();
            }
        }
	}
}
