// Copyright 2021, Infima Games. All Rights Reserved.

using System.Linq;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class Movement : MovementBehaviour
    {
       
        [SerializeField]
        private AudioClip audioClipWalking;

        [SerializeField]
        private AudioClip audioClipRunning;

        [SerializeField]
        private float speedWalking = 5.0f;

        [SerializeField]
        private float speedRunning = 9.0f;

     

   

        //Velocity.
        private Vector3 Velocity
        {
            //Getter.
            get => rigidBody.velocity;
            //Setter.
            set => rigidBody.velocity = value;
        }




        private Rigidbody rigidBody;
        private CapsuleCollider capsule;
        private AudioSource audioSource;
        private bool grounded;
        private CharacterBehaviour playerCharacter;
        private WeaponBehaviour equippedWeapon;
        private readonly RaycastHit[] groundHits = new RaycastHit[8];







        protected override void Awake()
        {
            //Get Player Character.
            playerCharacter = ServiceLocator.Current.Get<IGameModeService>().GetPlayerCharacter();
        }









        /// Initializes the FpsController on start.
        protected override  void Start()
        {
            //Rigidbody Setup.
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            //Cache the CapsuleCollider.
            capsule = GetComponent<CapsuleCollider>();

            //Audio Source Setup.
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = audioClipWalking;
            audioSource.loop = true;
        }





        /// Checks if the character is on the ground.
        private void OnCollisionStay()
        {
            //Bounds.
            Bounds bounds = capsule.bounds;
            //Extents.
            Vector3 extents = bounds.extents;
            //Radius.
            float radius = extents.x - 0.01f;
            
            //Cast. This checks whether there is indeed ground, or not.
            Physics.SphereCastNonAlloc(bounds.center, radius, Vector3.down,
                groundHits, extents.y - radius * 0.5f, ~0, QueryTriggerInteraction.Ignore);
            
            //We can ignore the rest if we don't have any proper hits.
            if (!groundHits.Any(hit => hit.collider != null && hit.collider != capsule)) 
                return;
            
            //Store RaycastHits.
            for (var i = 0; i < groundHits.Length; i++)
                groundHits[i] = new RaycastHit();

            //Set grounded. Now we know for sure that we're grounded.
            grounded = true;
        }





			
        protected override void FixedUpdate()
        {
            //Move.
            MoveCharacter();
            
            //Unground.
            grounded = false;
        }




        /// Moves the camera to the character, processes jumping and plays sounds every frame.
        protected override  void Update()
        {
            //Get the equipped weapon!
            equippedWeapon = playerCharacter.GetInventory().GetEquipped();
            
            //Play Sounds!
            PlayFootstepSounds();

            // Jumping 
            Jump(20000);

            
        }








        private void MoveCharacter()
        {
            #region Calculate Movement Velocity

            //Get Movement Input!
            Vector2 frameInput = playerCharacter.GetInputMovement();
            //Calculate local-space direction by using the player's input.
            var movement = new Vector3(frameInput.x, 0.0f, frameInput.y);
            
            //Running speed calculation.
            if(playerCharacter.IsRunning())
                movement *= speedRunning;
            else
            {
                //Multiply by the normal walking speed.
                movement *= speedWalking;
            }

            //World space velocity calculation. This allows us to add it to the rigidbody's velocity properly.
            movement = transform.TransformDirection(movement);

            #endregion
            
            //Update Velocity.
            if (grounded )
            {
                Velocity = new Vector3(movement.x, 0.0f, movement.z);

            }
            else
            {
               // Velocity = new Vector3(movement.x, -2f, movement.z);
                Velocity = new Vector3( movement.x , -10f, movement.z );
            }





                    
        }




        private void PlayFootstepSounds()
        {
            //Check if we're moving on the ground. We don't need footsteps in the air.
            if (grounded && rigidBody.velocity.sqrMagnitude > 0.1f)
            {
                audioSource.clip = playerCharacter.IsRunning() ? audioClipRunning : audioClipWalking;
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
            //FALLING!
            else if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }

        }


        protected private void Jump(float power)
        {
            if(grounded)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    rigidBody.AddForce(0, power, 0);
                    // add jumping animation
                }
            }
           else
            {
                    //print("im in the air!!!!!");

                // reduce y velocity
                // add falling animation
            }
        }






    }
}