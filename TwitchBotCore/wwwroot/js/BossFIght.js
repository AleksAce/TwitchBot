
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
    }
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
        bulletRect = bulletDiv.getBoundingClientRect();
        monsterOffset = bulletRect.top - monsterRect.top;
        console.log(monsterOffset);
        var id = setInterval(function () {
            bulletRect = bulletDiv.getBoundingClientRect();
            var nextPosition = bulletRect.left + bulletSpeed;
            bulletDiv.style.left = nextPosition + "px";
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

        }
        var clearBullet = function (bullet) {
            bullets.pop(bullet);

        }

    }
    //this.checkIntersection = function()
}

document.onclick = function () {
    fireOneBullet();
};

var fireOneBullet = function () {
    var bulletStartPosition = v2(catapultElement.style.left, catapultElement.style.bottom);
    var bullet1 = new bullet(bulletStartPosition.X, bulletStartPosition.Y, 10);
    bullet1.showBullet(catapultElement, bulletImgSrc);
   
    bullets.push(bullet1);
}


var startPage = function () {
    catapultImgSrc = document.getElementById("the-catapult").getAttribute("src");
    bossImgSrc = document.getElementById("the-boss").getAttribute("src");
   

   // bulletImgSrc = document.getElementById("the-catapult").getAttribute("src");
    var catapultMain = new catapult(10, 10);
    if (catapultMain.isInitiated == true) {

        catapultMain.showCatapult(catapultElement, catapultImgSrc);
    }
}();
