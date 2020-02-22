class Game {
    constructor () {
        this.balls = [new Ball(), new Ball()];
        this.environment = new TopDownEnvironment();
        this.players = [new ArrowKeyPlayer()];
        this.pb_interfaces = [new PlayerBallInterface()]
    }
}