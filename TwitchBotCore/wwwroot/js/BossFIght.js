
var bodyTag = document.getElementsByTagName('body');
var firingBody = document.getElementById("start-firing");
var catapultImgSrc = '/images/Test/Catapult.png';
var bulletImgSrc = '/images/Test/rocket.jpg';
var explosionImgSrc = '/images/BossFight/explosion.png';

var catapultElement = document.getElementById("catapult");
var monsterElement = document.getElementById("the-boss");
var monsterRect = monsterElement.getBoundingClientRect();

var bossHealthElement = document.getElementById("boss-health");
var bossHealth = bossHealthElement.getAttribute("data-value");
//Starting width
var bossHealthElementWidth = bossHealthElement.clientWidth;
bossHealth = bossHealth*1.0;

//The Ratio between the boss Hp and the element width,
// so we can multiply by the bullet damage and get the desired damage on the width
const REALHP_HPELEMENTWIDTH_RATIO = (bossHealthElement.clientWidth/bossHealth)*1.0;
const BULLET_DAMAGE = 1.0;



var bullets = [];
var catapult = function (posX, posY) {
    this.pos = v2(posX, posY);
    this.isInitiated = true;
    this.showCatapult = function (elementToStartOn, src) {
        elementToStartOn.style.bottom = this.pos.Y + "px";
        elementToStartOn.style.left = this.pos.X + "px";
        elementToStartOn.innerHTML = "<img  src='" + catapultImgSrc + "' />";
    };
};
// var id = setInterval(frame, 5);
var explode = function (position, elementToStartOn) {
    var explosionDiv = document.createElement('div');
    explosionDiv.className = "explosion-container";
    explosionDiv.style.top = position.Y + "px";
    explosionDiv.style.left = position.X + "px";
    explosionDiv.innerHTML = "<img class='explosion' src='" + explosionImgSrc + "' />";
    elementToStartOn.appendChild(explosionDiv);
    var timeExploding = 0;
    var explosionInterval = setInterval(function () {
        if (timeExploding > 100) {
            clearExplosionDiv(explosionDiv);
            clearInterval(explosionInterval);
            timeExploding = 0;
        }
        timeExploding += 40;
        explosionDiv.firstChild.style.width = timeExploding + 'px';
        explosionDiv.firstChild.style.height = timeExploding + 'px';

    }, 100);
    var clearExplosionDiv = function (Div) {
        Div.parentNode.removeChild(Div);

    };
}
var bullet = function (posX, posY, bulletSpeed) {
    this.pos = v2(posX, posY);
    this.isActive = true;
    this.speed = bulletSpeed;
    this.monsterOffset;
    this.bulletRect;
    this.showBullet = function (elementToStartOn, src) {
        //   elementToStartOn.
        var bulletDiv = document.createElement('div');
        bulletDiv.className = "bullet-container";
        bulletDiv.style.bottom = this.pos.Y + 'px';
        bulletDiv.style.left = this.pos.X + 'px';
        bulletDiv.innerHTML = "<img class='bullet' src='" + src + "' />";
        elementToStartOn.appendChild(bulletDiv);

        //VELOCITY
        //Calculate the direction here so the bullet's are not "following"
        bulletRect = bulletDiv.getBoundingClientRect();

        var randomPos = Math.floor(Math.random()*monsterRect.height) + 1;
        var middleLeftMonsterPos = new vec2(monsterRect.x, monsterRect.y + randomPos);

        var bulletPos = new vec2(bulletRect.x, bulletRect.y);
        var direction = middleLeftMonsterPos.subtract(bulletPos).normalize();

        var id = setInterval(function () {
            bulletRect = bulletDiv.getBoundingClientRect();
            bulletPos = new vec2(bulletRect.x, bulletRect.y);
            var nextPosition = new vec2(bulletPos.X + bulletSpeed * direction.X, bulletPos.Y + bulletSpeed * direction.Y);
            bulletDiv.style.left = nextPosition.X + "px";
            bulletDiv.style.top = nextPosition.Y + "px";
            //rectangles inersect
            if (bulletRect.left >= monsterRect.left) {
                
                //Explode
                bulletRect = bulletDiv.getBoundingClientRect();
                var explosionPosition = new vec2(bulletRect.left + bulletRect.width/2, bulletRect.top);
                explode(explosionPosition, elementToStartOn);
                
                //Lower boss HP
                if(bossHealth != null){
                    bossHealth -= 1;
                    updateBossHealth(bossHealthElement);
                    //If boss died
                    if(bossHealth <= 0){
                        //kill the boss
                        document.body.removeChild(document.querySelector("#target"));
                        //Set this to null so the game wont continue
                        bossHealth = null;
                        alert("boss is dead");
                        
                    }
                }

                //Clear the bullet
                clearBulletDiv(bulletDiv);
                clearBullet(this);
                clearInterval(id);
            }
        }, 10);
        
        var clearBulletDiv = function (Div) {
            Div.parentNode.removeChild(Div);

        };
        
        var clearBullet = function (bullet) {
            bullets.pop(bullet);

        };

    };
    //this.checkIntersection = function()
};

document.onclick = function () {
    fireOneBullet(bulletImgSrc);
};

var fireOneBullet = function (imgSrc, speed) {
    //right-corner                         
    var bulletStartPosition = v2(catapultRect.width + catapultRect.x, catapultRect.bottom - catapultRect.top);
    var bullet1 = new bullet(bulletStartPosition.X, bulletStartPosition.Y, speed);
    bullet1.showBullet(catapultElement, imgSrc);

    bullets.push(bullet1);
};
var updateBossHealth = function(HealthElement){
    bossHealthElementWidth -=REALHP_HPELEMENTWIDTH_RATIO*BULLET_DAMAGE;
    HealthElement.style.width = bossHealthElementWidth + "px";
    HealthElement.textContent = bossHealth;
};


var startPage = function () {
    
   
    //The Game
    catapultImgSrc = document.getElementById("the-catapult").getAttribute("src");
    bossImgSrc = document.getElementById("the-boss").getAttribute("src");

    if(bossHealth == 0){
        //Note: set this to null so we wont have a boss killing.. just fire bullets
        //Later check if bosshealth is null;
        bossHealthElement.textContent = "Undefined";
        bossHealth = null;
    }
    else{
        bossHealthElement.textContent = bossHealth;
    }

   // bulletImgSrc = document.getElementById("the-catapult").getAttribute("src");
    var catapultMain = new catapult(10, 10);
    catapultRect = catapultElement.getBoundingClientRect();
    if (catapultMain.isInitiated === true) {

        catapultMain.showCatapult(catapultElement, catapultImgSrc);
    }
    var interval = setInterval(getEmotes, 100);

    //Handle emotes
    var emotes = [];

    function getEmotes() {
        $.ajax({
            type: 'GET',
            url: '/BossFight?handler=Emotes',
            contentType: "application/json",
            success: function (data) {
                if (data.length > 0) {
                    $.each(data, function (i, item) {
                        emotes.push(item);
                    });
                    speed = emotes.length;
                    
                }
                
            }
        });
    };

    //Fire an emote every 8 ms
    setInterval(fireEmotes, 12);
    function fireEmotes() {
        if (emotes.length > 0) {
            if(bossHealth != null)
            {
            fireOneBullet(emotes[0],2*speed);
            emotes.shift();
            }
        }
    };
  
}();
