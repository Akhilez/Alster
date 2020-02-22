class P5Object {

  constructor() {
    this.mass = 1;
    this.maxVelocity = 10;

    this.location = createVector(width / 2, height / 2);
    this.velocity = createVector(0.0, 0.0);
    this.acceleration = createVector(0.0, 0.0);
  }

  setVelocity(velocity) {
    this.velocity = velocity;
    this.velocity.limit(this.maxVelocity);
  }

  applyForce(force) {
    let acceleration = p5.Vector.div(force, this.mass);
    this.acceleration.add(acceleration);
  }

  update() {
    this.setVelocity(p5.Vector.add(this.velocity, this.acceleration));
    this.location.add(this.velocity);
    this.acceleration.mult(0);
  }

  display() {
    point(this.location.x, this.location.y);
  }

}
