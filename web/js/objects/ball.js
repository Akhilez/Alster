class Ball extends P5Object {

  constructor(radius=10, props) {
    super(props);
    this.radius = radius;
    this.mass = 1;
  }

  update() {
    this.checkBounds();
    super.update();
  }

  display() {
    stroke(0);
    fill(175);
    ellipse(this.location.x, this.location.y, this.radius, this.radius);
  }

  checkBounds() {
    if (this.location.x > width || this.location.x < 0) {
      this.velocity.x = this.velocity.x * -0.9;
      let reactionForce = p5.Vector.mult(this.acceleration, this.mass);
      let oppositeForce = createVector(reactionForce.x * -1, reactionForce.y);
      this.applyForce(oppositeForce);
    }
    if (this.location.y > height || this.location.y < 0) {
      this.velocity.y = this.velocity.y * -0.9;
      let reactionForce = p5.Vector.mult(this.acceleration, this.mass);
      let oppositeForce = createVector(reactionForce.x, reactionForce.y * -1);
      this.applyForce(oppositeForce);
    }

  }

}
