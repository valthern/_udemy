function pruebaPuntoNetStatic() {
    DotNet.invokeMethodAsync("BlazorPeliculas.Client", "ObtenerCurrentCount")
        .then(resultado => {
            console.log("conteo desde javascript: " + resultado);
        })
}

function pruebaPuntoNetInstancia(dotNetHelper) {
    dotNetHelper.invokeMethodAsync("IncrementCount");
}

function timerInactivo(dotNetHelper) {
    var timer;
    let unSegundo = 1000;
    let segundosEnUnMinuto = 60;
    document.onmousemove = resetTimer;
    document.onkeypress = resetTimer;

    function resetTimer() {
        clearTimeout(timer);
        //timer = setTimeout(logout, unSegundo * segundosEnUnMinuto * 5);
    }

    function logout() {
        dotNetHelper.invokeMethodAsync("Logout");
    }
}
