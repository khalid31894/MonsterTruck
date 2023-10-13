using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    // Car rigidbody COM
    public Transform centerOfMass;

    // Wheel motor for drive the car based on wheelJoint 2D
    JointMotor2D[] motorBack;

    // Which wheel to drive with that?
    public WheelJoint2D[] motorWheel;

    // Store car speed
    public float speed;

    // Store is grounded based on the distance of the ground

    public bool Using = false;

    // How much distance of the ground means cars is grounded? answer=> less than 2.1f
    public float groundDistance = 2.1f;

    // Motor power, Brake power and deceleration speed
    public float motorPower = 1400f,
    brakePower = -14f,
     decelerationSpeed = 10000f;

    // Car max speed
    public float maxSpeed = 14f;

    // inrenal usage
    float motorTemp;

    // Can rotate option. be true value when car is on the fly
    bool canRotate = false;
    // Rotate force on the  fly 
    public float RotateForce = 140f;

    public AudioSource EngineSoundS;

    public bool isMobile;

    public ParticleSystem[] wheelParticle;

    // Internal usage
    float powerTemp;
    ParticleSystem.EmissionModule[] em;
    ParticleSystem.EmissionModule emSmoke;

    //public Transform particlePosition;
    public bool finishGateReached = false, TargetAchieved = false, Stopcar = false;

    public bool useSmoke;
    public ParticleSystem smoke;
    public float smokeTargetSpeed = 17f;

    public float cameraDistance = 15f;
    public bool forward = false, backward = false, ringF = false;
    float currentSpeed;
    public int JumpPower = 500;
    public bool UseTirePBack = true, UseTirePF = true;
    public bool IsGamePlay = false;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ReadyForAd = false;
        motorWheel = GetComponents<WheelJoint2D>();
        Vector3 posCamera;
        posCamera = Camera.main.transform.position;
        Camera.main.transform.position = new Vector3(posCamera.x, posCamera.y, -cameraDistance);
        // Set car rigidbody's COM
        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass.transform.localPosition;

        // Starting with WheelJoint2D motor
        motorBack = new JointMotor2D[motorWheel.Length];
        for (int i = 0; i < motorWheel.Length; i++)
        {
            motorBack[i] = motorWheel[i].motor;
        }
        // Cast a ray to find isGrounded 
        //StartCoroutine(RaycCast());

        EngineSoundS = GetComponent<AudioSource>();

        powerTemp = motorPower;
        em = new ParticleSystem.EmissionModule[wheelParticle.Length];
        for (int i = 0; i < em.Length; i++)
        {
            em[i] = wheelParticle[i].emission;
            em[i].enabled = false;
        }

        if (smoke)
        {
            emSmoke = smoke.emission;
            emSmoke.enabled = false;
        }
    }
    [System.Obsolete]
    public void CheckJump()
    {

        if (IsJump)
        {
          //  AnimatorHandler.Instance.PlaySmile();
           SoundManager.instance.PlayEffect_Complete(15);
            GetComponent<Rigidbody2D>().AddForce(transform.up * JumpPower, ForceMode2D.Impulse);
            GameManager.instance.Jump.interactable = false;

            IsJump = false;

        }
    }
    public bool IsJump = true;
    void JumpLoad()
    {
        if (GameManager.instance)
        {
            IsJump = true;
            GameManager.instance.Jump.interactable = true;
        }
    }
    public bool Down = true;
    void FloorParticles()
    {
        //if (isGrounded)
        //{
        
        if (speed < 4.3f)
        {
            if (UseTirePBack)
                em[0].enabled = true;
            if (UseTirePF)
                em[1].enabled = true;
        }
        else
        {

            SoundManager.instance.PlayEffect_Loop(12);
            if (UseTirePBack)
                em[0].enabled = false;
            if (UseTirePF)
                em[1].enabled = false;
        }
        //}
        //else
        //{ 
        //    if (UseTirePBack)
        //            em[0].enabled = false;
        //        if (UseTirePF)
        //            em[1].enabled = false;
        //}
    }
    void FloorParticleStop(float s)
    {
        //if (isGrounded)
        {
            if (s > 1)
            {
                if (UseTirePBack)
                    em[0].enabled = true;
                if (UseTirePF)
                    em[1].enabled = true;
            }
            else
            {
                SoundManager.instance.PlayEffect_Loop(12);
                if (UseTirePBack)
                    em[0].enabled = false;
                if (UseTirePF)
                    em[1].enabled = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (IsGamePlay)
        {
            if (Car_Handler.instance)
            {
                if (Car_Handler.instance.Belt)
                {
                    if (Mathf.Abs(speed) > 0.1f)
                    {
                        Car_Handler.instance.Belt.enabled = true;
                    }
                    else
                    {
                        Car_Handler.instance.Belt.enabled = false;
                    }
                }
            }
            if (speed > maxSpeed)
                motorPower = 0;
            else
                motorPower = powerTemp;

            // Moving forward
            if (!finishGateReached)
            {
                //if (Grounded.IsGrounded) { }
                if (motorBack[0].motorSpeed > Mathf.Abs(1f) && forward && backward && Grounded.IsGrounded)
                {
                    StopCar();
                }
                else if (Down)
                {

                    if (Input.GetAxis("Horizontal") > 0 || HoriTemp > 0)
                    {
                        if (SceneManager.GetActiveScene().name != "MainMenu")
                        {
                            if (GameManager.instance.play != null)
                            {
                                if (GameManager.instance.play.activeInHierarchy)
                                {
                                    //GameManager.instance.play.GetComponent<Image>().DOFade(50, 2f).OnComplete(delegate
                                    //{
                                        //GameManager.instance.play.SetActive(false);
                                       // GameManager.instance.play.GetComponent<Animator>().enabled = false;
                                        GameManager.instance.Jump.gameObject.SetActive(true);
                                    //});
                                }
                            }
                        }
                        // Add force to car back wheel
                        //if (isGrounded)
                        // {
                        //print("forward");
                        //motorBack[0].motorSpeed = Mathf.Lerp(motorBack[0].motorSpeed, -motorPower, Time.deltaTime * 1.4f);
                        //motorBack[1].motorSpeed = Mathf.Lerp(motorBack[1].motorSpeed, -motorPower, Time.deltaTime * 1.4f);
                        if (Grounded.IsGrounded == true)
                        {
                        for (int i = 0; i < motorBack.Length; i++) 
                            {  
                                motorBack[i].motorSpeed = Mathf.Lerp(motorBack[i].motorSpeed, -motorPower, Time.deltaTime);
                            }
                        }

                        //}
                        // Wheel particles

                        FloorParticles();

                    }
                    else
                    {// Moving backward


                        if (Input.GetAxis("Horizontal") < 0 || HoriTemp < 0)
                        {
                            //if (AnimatorHandler.Instance.idle&& Grounded.IsGrounded)
                            //{
                            //    AnimatorHandler.Instance.CarBack();
                            //}
                            //else
                            //{
                            //    AnimatorHandler.Instance.idle=false;
                            //    AnimatorHandler.Instance.SetIdle();
                            //}
                           // print("backward");
                            if (speed < -maxSpeed)
                            {
                                //if (isGrounded)
                                {
                                    //motorBack[0].motorSpeed = Mathf.Lerp(motorBack[0].motorSpeed, 0, Time.deltaTime * 3f);
                                    //motorBack[1].motorSpeed = Mathf.Lerp(motorBack[1].motorSpeed, 0, Time.deltaTime * 3f);
                                    if (transform.rotation.eulerAngles.z > 15)
                                    {
                                        Debug.Log(transform.rotation.eulerAngles.z);
                                    }

                                    for (int i = 0; i < motorBack.Length; i++)
                                    {
                                        motorBack[i].motorSpeed = Mathf.Lerp(motorBack[i].motorSpeed, 0, Time.deltaTime * 3f);
                                    }
                                }
                            }
                            else
                            {
                                //if (isGrounded)
                                {
                                    for (int i = 0; i < motorBack.Length; i++)
                                    {
                                        //motorBack[0].motorSpeed = Mathf.Lerp(motorBack[0].motorSpeed, motorPower, Time.deltaTime * 1.4f);
                                        //motorBack[1].motorSpeed = Mathf.Lerp(motorBack[1].motorSpeed, motorPower, Time.deltaTime * 1.4f);
                                        motorBack[i].motorSpeed = Mathf.Lerp(motorBack[i].motorSpeed, motorPower, Time.deltaTime );
                                    }
                                }
                            }
                        }
                        else
                        {// Releasing car throttle and brake
                            if (GetComponent<Rigidbody2D>().velocity.magnitude < 1 && !ringF)
                            {

                                forward = false;
                                backward = false;
                                if (AnimatorHandler.Instance != null)
                                {
                                    AnimatorHandler.Instance.SetIdle();
                                }
                            }
                            if (Car_Handler.instance)
                            {
                                if (!Using && Car_Handler.instance.flag == false)
                                {
                                    for (int i = 0; i < em.Length; i++)
                                    {
                                        em[i].enabled = false;
                                    }
                                    for (int i = 0; i < motorBack.Length; i++)
                                    {
                                        //motorBack[0].motorSpeed = Mathf.Lerp(motorBack[0].motorSpeed, 0, Time.fixedDeltaTime * decelerationSpeed);
                                        //motorBack[1].motorSpeed = Mathf.Lerp(motorBack[1].motorSpeed, 0, Time.fixedDeltaTime * decelerationSpeed);
                                        motorBack[i].motorSpeed = Mathf.Lerp(motorBack[i].motorSpeed, 0, Time.fixedDeltaTime * decelerationSpeed);
                                    }
                                }
                                if (Mathf.Abs(motorBack[0].motorSpeed) > 0 && Car_Handler.instance.flag)
                                {
                                    for (int i = 0; i < motorBack.Length; i++)
                                    {
                                        //motorBack[0].motorSpeed = Mathf.Lerp(motorBack[0].motorSpeed, 0, decelerationSpeed);
                                        //motorBack[1].motorSpeed = Mathf.Lerp(motorBack[1].motorSpeed, 0, decelerationSpeed);
                                        motorBack[i].motorSpeed = Mathf.Lerp(motorBack[i].motorSpeed, 0, decelerationSpeed);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (finishGateReached && !TargetAchieved)
            {
                for (int i = 0; i < motorBack.Length; i++)
                {
                    //motorBack[0].motorSpeed = Mathf.Lerp(motorBack[0].motorSpeed, -1000, Time.deltaTime * 5f);
                    //motorBack[1].motorSpeed = Mathf.Lerp(motorBack[1].motorSpeed, -1000, Time.deltaTime * 5f);
                    motorBack[i].motorSpeed = Mathf.Lerp(motorBack[i].motorSpeed, -3000, Time.deltaTime * 5f);
                }
                AnimatorHandler.Instance.SetIdle();
                AnimatorHandler.Instance.idle = false;
                //GameManager.instance.Jump.gameObject.SetActive(false);
                //LoadScene.instance.End_Gate();
                GameManager.instance.Race.SetActive(false);
                GameManager.instance.Break.SetActive(false);
                Debug.Log("Finish Gate Reached" + finishGateReached);
            }
            else if (TargetAchieved && !Stopcar)
            {
                if (!ended)
                {
                    Down = false;
                    StopCar();
                    ended = true;
                    SoundManager.instance.PlayEffect_Instance(27);
                    Invoke(nameof(SceneLoad), 5f);
                    Debug.Log("Target Achived" + TargetAchieved);
                    //GameManager.instance.particle.Play();
                }


            }
            else if (Stopcar)
            {
                rb.velocity = Vector2.zero;
            }

            for (int i = 0; i < motorWheel.Length; i++)
            {
                //motorWheel[0].motor = motorBack[0];
                // motorWheel[1].motor = motorBack[1];
                motorWheel[i].motor = motorBack[i];
            }
            // Cheack fo rotate on the fly
            //Rotate();


            //#if UNITY_EDITOR
            //            EngineSoundEditor();
            //#else
            //		EngineSoundMobile (); 
            //#endif

            if (!isMobile)
                HoriTemp = Input.GetAxis("Horizontal");

            if (useSmoke)
            {
                if (Input.GetAxis("Horizontal") > 0 || HoriTemp > 0)
                {
                    if (speed < smokeTargetSpeed)
                        emSmoke.enabled = true;
                    else
                        emSmoke.enabled = false;
                }
                else
                    emSmoke.enabled = false;
            }
        }
    }
    public bool ended = false;
    //public float slowdownRate = .1f;
    void StopCar()
    {
        //Using = true;
        Down = false;
        //print(currentSpeed);
        SoundManager.instance.PlayEffect_Complete(10);
        SoundManager.instance.StopEffect(12);
        speed = GetComponent<Rigidbody2D>().velocity.magnitude;
        if (speed < 0)
            speed = speed * -1;
       // print("InStop " + speed);

        FloorParticleStop(speed);
        if (speed > 1)
        {
            for (int i = 0; i < motorBack.Length; i++)
            {
                //motorBack[0].motorSpeed = Mathf.Lerp(motorBack[0].motorSpeed, 0, 10000);
                //motorBack[1].motorSpeed = Mathf.Lerp(motorBack[1].motorSpeed, 0, 10000);
                motorBack[i].motorSpeed = Mathf.Lerp(motorBack[i].motorSpeed, 0, 10000);
            }
        }
        else
        {
            forward = false;
            backward = false;

            Down = true;
        }
    }
    [System.Obsolete]
    public void AdJump()
    {
        if (SceneManager.GetActiveScene().name != "Car_Paint" && SceneManager.GetActiveScene().name != "MainMenu")
        {
            print("JumpAdded");
            GameManager.instance.Jump.onClick.AddListener(() => CheckJump());
        }
    }
    void LateUpdate()
    {
        if (IsGamePlay)
        {
            // Get car speed
            speed = GetComponent<Rigidbody2D>().velocity.magnitude;
            //print(speed);
            //print(speed);
            if (Input.GetAxis("Horizontal") < 0 || HoriTemp < 0)
                speed = -speed;
            //print(speed);
        }
    }

    // Rotate car on air based on speed
    void Rotate()
    {
        //based on player forward input(Like Hill Climb Racing game)
        if (Input.GetAxis("Horizontal") > 0 || HoriTemp > 0)
        {

            GetComponent<Rigidbody2D>().AddTorque(RotateForce);
        }
        else
        {
            if (Input.GetAxis("Horizontal") < 0.0f || HoriTemp < 0)
            {
                GetComponent<Rigidbody2D>().AddTorque(-RotateForce);
            }
        }
    }
    // Raycast body to find that car is on the ground or not


    // Engine sound system

    public float Multiplyer = 3f;
    public float minP = 1f;
    public float maxP = 2.4f;
    float HoriTemp;

    [System.Obsolete]
    public void EngineSoundMin()
    {
        if (EngineSoundS.pitch > minP)
            EngineSoundS.pitch -= 1.4f * Time.deltaTime;
    }

    [System.Obsolete]
    public void EngineSoundMobile()
    {

        if (speed < 40)
        {
            EngineSoundS.pitch = Mathf.Lerp(EngineSoundS.pitch, Mathf.Clamp(HoriTemp * Multiplyer, minP, maxP), Time.deltaTime * 5);
        }
        else
        {
            EngineSoundS.pitch = Mathf.Lerp(EngineSoundS.pitch, Mathf.Clamp(HoriTemp * Multiplyer, minP, maxP), Time.deltaTime * 5);
        }
    }

    [System.Obsolete]
    public void EngineSoundEditor()
    {

        if (speed < 40)
        {
            EngineSoundS.pitch = Mathf.Lerp(EngineSoundS.pitch, Mathf.Clamp(Input.GetAxis("Horizontal") * Multiplyer, minP, maxP), Time.deltaTime * 5);
        }
        else
        {
            EngineSoundS.pitch = Mathf.Lerp(EngineSoundS.pitch, Mathf.Clamp(Input.GetAxis("Horizontal") * Multiplyer, minP, maxP), Time.deltaTime * 5);
        }
    }

    // Vehicle input system
    //this is public function for car Acceleration UI Button
    [System.Obsolete]
    public void Acceleration()
    {
        HoriTemp = 1f;
    }
    //this is public function for car Brake\Backward UI Button
    [System.Obsolete]
    public void Brake()
    {
        HoriTemp = -1f;
    }


    //this is for when player both release Brake or Acceleration button
    [System.Obsolete]
    public void GasBrakeRelease()
    {
        HoriTemp = 0;
    }
    bool isDone = true;
    public static bool ReadyForAd = false;
    void SceneLoad()
    {
        //if (!ReadyForAd)
        {
            ReadyForAd = true;
            //Car_Handler.instance.flag = true;
            //gameObject.GetComponent<CarController>().enabled = false;
            DOTween.KillAll();
         
                Destroy(transform.parent.gameObject);
            //IntitializeAdmobAds_CB._instance.ShowAdmobInterstialAd(); // from game end to main manu
            LoaderCallBack.showMeAd = true;

            SceneLoader.LoadScene(SceneLoader.Scenes.Scene2_Menu);

            isDone = false;
        }

    }

}