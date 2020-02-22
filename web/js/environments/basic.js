class Environment {
  constructor() {
    this.backgroundColor = 240;
    this.forces = [];
  }

  display() {
    background(this.backgroundColor);
  }

  applyForces(...objects) {
    for (const object of objects) {
      for (const force in this.forces) {
        object.applyForce(this.forces[force]);
      }
    }
  }
}

class TopDownEnvironment extends Environment {
  constructor() {
    super();
  }
}

class BasicEnvironment extends Environment {
  constructor() {
    super();
    this.forces = {
      wind: createVector(-0.01, 0),
      gravity: createVector(0, 0.1),
    }
  }
}
