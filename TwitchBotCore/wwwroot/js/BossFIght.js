
var bodyTag = document.getElementsByTagName('body');
var firingBody = document.getElementById("start-firing");
var catapultImgSrc = '/images/Test/Catapult.png';
var bulletImgSrc = '/images/Test/rocket.jpg';

var catapultElement = document.getElementById("catapult");
var monsterElement = document.getElementById("the-boss");
var monsterRect = monsterElement.getBoundingClientRect();


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
        bulletDiv.style.bottom = this.pos.Y;
        bulletDiv.style.left = this.pos.X;
        bulletDiv.innerHTML = "<img class='bullet' src='" + src + "' />";
        elementToStartOn.appendChild(bulletDiv);

        //VELOCITY
        //Calculate the direction here so we won't stop
        bulletRect = bulletDiv.getBoundingClientRect();
        //Randomize this
        var middleLeftMonsterPos = new vec2(monsterRect.x, monsterRect.y + monsterRect.height / 2);

        // var monsterPos = new vec2(monsterRect.x, monsterRect.y);
        var bulletPos = new vec2(bulletRect.x, bulletRect.y);
        var direction = middleLeftMonsterPos.subtract(bulletPos).normalize();

        var id = setInterval(function () {
            bulletRect = bulletDiv.getBoundingClientRect();
            bulletPos = new vec2(bulletRect.x, bulletRect.y);
            var nextPosition = new vec2(bulletPos.X + bulletSpeed * direction.X, bulletPos.Y + bulletSpeed * direction.Y);
            bulletDiv.style.left = nextPosition.X + "px";
            bulletDiv.style.top = nextPosition.Y + "px";
            if (bulletRect.left >= monsterRect.left) {
                //inersect
                //Explode
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
catapultRect = catapultElement.getBoundingClientRect();
var fireOneBullet = function (imgSrc, speed) {
    //right-corner                         
    var bulletStartPosition = v2(catapultRect.width + catapultRect.x, catapultRect.bottom - catapultRect.top);
    var bullet1 = new bullet(bulletStartPosition.X, bulletStartPosition.Y, speed);
    bullet1.showBullet(catapultElement, imgSrc);

    bullets.push(bullet1);
};



var startPage = function () {
    
   
    //The Game
    catapultImgSrc = document.getElementById("the-catapult").getAttribute("src");
    bossImgSrc = document.getElementById("the-boss").getAttribute("src");

   // bulletImgSrc = document.getElementById("the-catapult").getAttribute("src");
    var catapultMain = new catapult(10, 10);
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
            
            fireOneBullet(emotes[0],2*speed);
            emotes.shift();
        }
    };
  
}();
