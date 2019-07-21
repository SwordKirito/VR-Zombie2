#pragma strict
private var sfx:boolean=false;
private var sfxSpeed=2.0;
private var gun:GameObject;

var bayonet:boolean=true;
var gun01:boolean=false;
var gun02:boolean=false;
var gun03:boolean=false;
var gun04:boolean=false;
var gun05:boolean=false;

var bayonetPrefab:GameObject;
var Gun01Prefab:GameObject;
var Gun02Prefab:GameObject;
var Gun03Prefab:GameObject;
var Gun04Prefab:GameObject;
var Gun05Prefab:GameObject;

var walkSound: AudioClip;
var jumpSound: AudioClip;



function Start () {
loadGun (bayonetPrefab);
}

function LateUpdate () {

 
var getFire=Input.GetButton ("Fire1")||Input.GetButton ("Fire2")||Input.GetButton ("Fire3");
var getMove =Input.GetAxisRaw("Vertical")||Input.GetAxisRaw("Horizontal");
var jump=Input.GetButton ("Jump");


if (Input.GetKeyDown ("0"))
{
 Destroy (gun);
 loadGun (bayonetPrefab);
 }
 
 
if (Input.GetKeyDown ("1"))
{ 
  Destroy (gun);
  loadGun (Gun01Prefab);
 
}
if (Input.GetKeyDown ("2"))
{
  Destroy (gun);
  loadGun (Gun02Prefab);
 
}
if (Input.GetKeyDown ("3"))
{
  Destroy (gun);
  loadGun (Gun03Prefab);
 
}

if (Input.GetKeyDown ("4"))
{
  Destroy (gun);
  loadGun (Gun04Prefab);
 
}
if (Input.GetKeyDown ("5"))
{
  Destroy (gun);
  loadGun (Gun05Prefab);
 
}


if (getMove)
	{
	if (!sfx&& !jump)
	playSound (walkSound,sfxSpeed);

	}
 
if (jump)
	{
	if (!sfx){
	playSound (jumpSound,0.50);
	}
	}
	 
if (!jump&&  getMove==0 &&!getFire)
	{
	

	}
}


function playSound (soundClip:AudioClip, speed:float){

      	this.GetComponent.<AudioSource>().clip = soundClip;
      	this.GetComponent.<AudioSource>().pitch=speed; 
 		this.GetComponent.<AudioSource>().Play();
 		sfx=true;
 		yield WaitForSeconds (soundClip.length/speed);
 		sfx=false;
   }
 
 function waitTime (wTime:float)
 {
 
 yield WaitForSeconds (wTime);
 }
 
 
 function loadGun (gunPrefab:GameObject) {
	gun =Instantiate (gunPrefab, this.transform.localPosition+gunPrefab.transform.localPosition, gunPrefab.transform.localRotation);
 
	gun.transform.parent=this.transform;
	gun.transform.localPosition=gunPrefab.transform.localPosition;
	gun.transform.localRotation=gunPrefab.transform.localRotation;
	
	}