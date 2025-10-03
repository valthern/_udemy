function pruebaPuntoNetStatic() {
    DotNet.invokeMethodAsync("BlazorPeliculas.Client", "ObtenerCurrentCount")
        .then(resultado => {
            console.log("conteo desde javascript: " + resultado);
        })
}

function pruebaPuntoNetInstancia(dotNetHelper) {
    dotNetHelper.invokeMethodAsync("IncrementCount");
}