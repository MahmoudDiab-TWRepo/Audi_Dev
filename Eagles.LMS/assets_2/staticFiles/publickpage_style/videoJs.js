var player = videojs('my-video', {
    autoplay: false,
    controls: true,
    loop: true,
    playbackRates: [0.25, 0.5, 1, 1.5, 2.5, 3.5, 4],
    plugins: {
        hotkeys: {
            enableModifiersForNumbers: false,
            seekStep: 10,
            volumeStep: 0.1,
        }

    }
});

player.mobileUi({
    fullscreen: {
        enterOnRotate: true,
        exitOnRotate: true,
        lockOnRotate: true
    },
    touchControls: {
        seekSeconds: 10,
        tapTimeout: 300,
        disableOnEnd: false
    }
});

