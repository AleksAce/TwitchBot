var v2 = function (x, y) {
    return new vec2(x, y);
};
var vec2 = function (x, y) {
    this.X = x || 0;
    this.Y = y || 0;
};
vec2.prototype.add = function (v2) {
    return new vec2(this.X + v2.Y, this.Y + v2.Y);
}
vec2.prototype.subtract = function (v2) {
    return new vec2(this.X - v2.X, this.Y - v2.Y);
}
vec2.prototype.multiply = function (val) {
    return new vec2(this.X * val, this.Y * val);

}
vec2.prototype.divide = function (val) {
    return new vec2(this.X / val, this.Y / val);

}
vec2.prototype.dotProduct = function (v2) {
    return this.X * v2.X + this.Y * v2.Y;

}
vec2.prototype.normalize = function () {
    var magnitude = getMagnitude(this);
    return v2(this.X / magnitude, this.Y / magnitude);

}
vec2.prototype.toString = function () {
    return "x" + this.X + "y" + this.Y;

}

vec2.prototype.GetAngleInRadians = function () {
    return Math.atan2(this.Y, this.X);

}
vec2.prototype.getMagnitude = function () {
    return Math.sqrt(this.X * this.X + this.Y * this.Y);

}
