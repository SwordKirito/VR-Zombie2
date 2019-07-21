#pragma strict
private var sfx:boolean=false;
private var afx:boolean=false;
private var bfx:boolean=false;
private var Rcnt=0;
var GChange:boolean=false;

private var GunFireSpr:GameObject;
private var idleCnt:int=0;
private var GunFireLight:Light; 

var reload:boolean=false;

var startClip:AnimationClip;
var idleClip:AnimationClip;
var walkClip:AnimationClip;
var attackClip:AnimationClip;
var shootClip:AnimationClip;
var oneShootClip:AnimationClip;
var reloadClip:AnimationClip;

private var gunStart:boolean=false;

var attackSound: AudioClip;
var shootSound: AudioClip;
var reloadSound: AudioClip;

var animSpeed:float=1.0;
var prefabBullet:Transform;

function Start () {
gunStart=false;
if (GameObject.Find ("GunFire"))
	{
  	GunFireSpr=GameObject.Find ("GunFire");
  	GunFireLight=GameObject.Find ("GunFireLight").GetComponent(Light); 
	GunFireSpr.GetComponent.<Renderer>().enabled=false;
	GunFireLight.intensity=0;
	}
	
	setClips();
	setAnimSpeed(animSpeed);



if (startClip) {
 this.GetComponent.<Animation>().Play  (startClip.name); 
 yield WaitForSeconds (startClip.length);
 gunStart=true;
 }
 
 }

function LateUpdate () {
if (GChange){

 		gunStart=false;
 		if (!afx){
 		animBlend( startClip);
 		playAnim ( startClip);
 		}
 		
	}	
	
if (gunStart){
		if (GameObject.Find ("GunFire"))
			{
			GunFireSpr=GameObject.Find ("GunFire");
  			GunFireLight=GameObject.Find ("GunFireLight").GetComponent(Light); 
	 	 	GunFireSpr.GetComponent.<Renderer>().enabled=false;
		 	GunFireLight.intensity=0;
		 	}

var reloadKey=Input.GetKeyDown ("r");

if (reloadKey){
	reload=true;
	Rcnt=0;
	}
	
	

	
var getFire=(Input.GetButton ("Fire1")||Input.GetButton ("Fire2")||Input.GetButton ("Fire3"));
var getMove=(Input.GetAxisRaw("Vertical")||Input.GetAxisRaw("Horizontal"));
var jump=Input.GetButton ("Jump");

 if (this.reload){
	if (reloadClip){
		if (!sfx&& Rcnt==0){
		playSound (reloadSound);
		animBlend( reloadClip);
 		playAnim ( reloadClip);
		Rcnt++;
			}
 		}
 		idleCnt=0;
	}

  
  if (!jump&&  getMove==0 &&!getFire&& !sfx)
	{
	idleCnt++;
	animBlend( idleClip);
	if (idleCnt>100){
		animBlend( idleClip);
 		playAnim ( idleClip);
	}
}
  
  
if ( getMove && !getFire && reload==false){

	if ( walkClip){
 		animBlend( walkClip);
 		playAnim ( walkClip);
		idleCnt=0;
		}
	}
	
 
if (getFire && reload==false)
{
	if (attackClip){
		playSound (attackSound);
 		animBlend( attackClip);
 		playAnim ( attackClip);
 		idleCnt=0;
 		}
 	else {
 	
 			if (oneShootClip)
 			{
 				
 			if (!bfx){
 				oneGunFire();
 				oneShoot();
 				onePlaySound (shootSound);
 				oneGunFire();
 				}
 				
 				if (!bfx)
 					
 					animBlend(oneShootClip);
 					playAnim (oneShootClip);
 	
 					}
 	
 	
 		if (shootClip)
 		{
 			gunFire();
 			if (!bfx)
 				shoot();
 				
 		if (!sfx)
 		playSound (shootSound);
 		animBlend(shootClip);
 		playAnim (shootClip);
 	
 			}
 			idleCnt=0;
	 	}
	}
 }

}


function setClips(){
	
	if (startClip)
	this.GetComponent.<Animation>().AddClip(startClip, startClip.name);
	if (idleClip)
	this.GetComponent.<Animation>().AddClip(idleClip, idleClip.name);
	if (walkClip)
	this.GetComponent.<Animation>().AddClip(walkClip, walkClip.name);
	
	if (attackClip)
	this.GetComponent.<Animation>().AddClip(attackClip, attackClip.name);
	
	if (shootClip)
	this.GetComponent.<Animation>().AddClip(shootClip, shootClip.name);
	
	if (oneShootClip)
	this.GetComponent.<Animation>().AddClip(oneShootClip, oneShootClip.name);
	
	
	if (reloadClip)
	this.GetComponent.<Animation>().AddClip(reloadClip, reloadClip.name);

}


function setAnimSpeed (speed:float)
{
for (var state : AnimationState in GetComponent.<Animation>()) {
    state.speed = speed;
	}
}

function playAnim(animName:AnimationClip)
{
 afx=true;
this.GetComponent.<Animation>().Play(animName.name);
yield WaitForSeconds (animName.length);
afx=false;
if (animName==reloadClip){
reload=false;
Rcnt=0;
}
}

function animBlend(animName:AnimationClip)
{
this.GetComponent.<Animation>().CrossFadeQueued(animName.name, 0.2);
yield WaitForSeconds (0.2);

}


function playSound (soundClip:AudioClip){
   this.GetComponent.<AudioSource>().clip = soundClip;
 	this.GetComponent.<AudioSource>().Play();
 	sfx=true;
    yield WaitForSeconds (soundClip.length);
    sfx=false;
   }
 
 
 function onePlaySound (soundClip:AudioClip){
   this.GetComponent.<AudioSource>().clip = soundClip;
 	this.GetComponent.<AudioSource>().Play();
 	sfx=true;
    yield WaitForSeconds (oneShootClip.length);
    sfx=false;
   }
 
 
 function oneGunFire (){
 
 GunFireSpr.GetComponent.<Renderer>().enabled=true;
 GunFireLight.intensity=7.0;
 
 }

 
function gunFire (){
var range= Random.RandomRange (0,2);
	
 if (range){
	 GunFireSpr.GetComponent.<Renderer>().enabled=true;


 }
 else 
	GunFireSpr.GetComponent.<Renderer>().enabled=false;
	GunFireLight.intensity=Random.RandomRange (0,8.0);

 }
 
 function shoot()
 {
        var instanceBullet = Instantiate(prefabBullet, GunFireSpr.transform.position , Quaternion.identity);
        instanceBullet.GetComponent.<Rigidbody>().AddRelativeForce ((GameObject.Find ("GunCamera").transform.forward) * 300 );
        bfx=true;
   		yield WaitForSeconds (0.1);
   		bfx=false;
    }

  function oneShoot()
 {
        var instanceBullet = Instantiate(prefabBullet, GunFireSpr.transform.position , Quaternion.identity);
        instanceBullet.GetComponent.<Rigidbody>().AddRelativeForce ((GameObject.Find ("GunCamera").transform.forward) * 300 );
        bfx=true;
        oneGunFire ();
   		yield WaitForSeconds (oneShootClip.length);
   		bfx=false;
    }
 