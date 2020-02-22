let balls;
let environment;
let players;
let pb_interfaces;

function setup() {
  createCanvas(640, 480);

  balls = [new Ball()];
  environment = new TopDownEnvironment();
  players = [new ArrowKeyPlayer()];
  pb_interfaces = [new PlayerBallInterface()]
}

function draw() {
  let playerActions = [];
  for (let player of players) {
    playerActions.push(player.captureActions());
  }



  environment.applyForces(balls);
  balls.forEach(item => item.update());

  environment.display();
  balls.forEach(item => item.display());

}

function keyPressed() {

}
