text#svg-text  {
    animation-name: animateSvg;
    transform-origin: 50% 50%;
    animation-duration: 5s;
    animation-timing-function: linear;
    animation-iteration-count: infinite;
}

@keyframes animateSvg  {
    from  {
        transform: rotate(0deg);
    }

    to  {
        transform: rotate(360deg);
    }
}