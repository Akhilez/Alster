actions = {
    up: 'UP',
    down: 'DOWN',
    left: 'LEFT',
    right: 'RIGHT',
};

class Player {
    captureActions () {}
}

class ArrowKeyPlayer extends Player {
    captureActions() {
        let actions = [];
        if (keyIsDown(UP_ARROW)) {
            actions.push(actions.up);
        } else if (keyIsDown(DOWN_ARROW)) {
            actions.push(actions.down);
        } else if (keyIsDown(LEFT_ARROW)) {
            actions.push(actions.left);
        } else if (keyIsDown(RIGHT_ARROW)) {
            actions.push(actions.right);
        }
        return actions;
    }
}

class WsadPlayer extends Player {

}
