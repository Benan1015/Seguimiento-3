class Platillo {
    constructor(id, nombre, categoria, precio, img) {
        this.id = id;
        this.nombre = nombre;
        this.categoria = categoria;
        this.precio = precio;
        this.img = img;
    }

    precioFormateado() {
        return `$${this.precio.toLocaleString('es-CO')}`;
    }
}

// Datos del Menú 
const menuRestaurante = [
    new Platillo(1, "Spaghetti Carbonara", "pastas", 28000, "https://images.unsplash.com/photo-1612874742237-6526221588e3?w=400"),
    new Platillo(2, "Lasagna Bolognese", "pastas", 32000, "https://images.unsplash.com/photo-1574894709920-11b28e7367e3?w=400"),
    new Platillo(3, "Pizza Margherita", "pizzas", 25000, "https://images.unsplash.com/photo-1604382354936-07c5d9983bd3?w=400"),
    new Platillo(4, "Pizza Pepperoni", "pizzas", 29000, "https://images.unsplash.com/photo-1628840042765-356cda07504e?w=400"),
    new Platillo(5, "Tiramisú Tradicional", "postres", 14000, "https://images.unsplash.com/photo-1571877227200-a0d98ea607e9?w=400"),
    new Platillo(6, "Panna Cotta", "postres", 12000, "https://images.unsplash.com/photo-1488477181946-6428a0291777?w=400")
];

// Arreglo para almacenar el pedido actual
let pedido = [];

//Captura de Elementos del DOM
const contenedorTarjetas = document.getElementById("contenedorTarjetas");
const inputBuscador = document.getElementById("inputBuscador");
const selectCategoria = document.getElementById("selectCategoria");
const tablaPedido = document.getElementById("tablaPedido");
const precioTotal = document.getElementById("precioTotal");
const btnVaciar = document.getElementById("btnVaciar");

// Mostrar platillos en el DOM mediante el ciclo for...of
function mostrarMenu(listaPlatillos) {
    contenedorTarjetas.innerHTML = "";

    if (listaPlatillos.length === 0) {
        contenedorTarjetas.innerHTML = `
            <div class="col-12 text-center text-muted py-4">
                <p class="fs-5 mb-0">No se encontraron platillos.</p>
            </div>
        `;
        return;
    }

    for (let platillo of listaPlatillos) {
        contenedorTarjetas.innerHTML += `
            <div class="col-12 col-md-6 col-lg-4">
                <div class="card h-100 shadow-sm card-platillo">
                    <img src="${platillo.img}" class="card-img-top" alt="${platillo.nombre}">
                    <div class="card-body d-flex flex-column">
                        <h5 class="card-title">${platillo.nombre}</h5>
                        <p class="card-text text-muted">Categoría: <span class="badge bg-secondary">${platillo.categoria}</span></p>
                        <div class="mt-auto d-flex justify-content-between align-items-center">
                            <span class="fw-bold fs-5 text-dark">${platillo.precioFormateado()}</span>
                            <button class="btn btn-danger btn-sm" onclick="agregarAlPedido(${platillo.id})">
                                + Agregar
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        `;
    }
}

// Filtrar por texto y categoría
function filtrarMenu() {
    const texto = inputBuscador.value.toLowerCase();
    const categoria = selectCategoria.value;

    let listaFiltrada = [];

    for (let platillo of menuRestaurante) {
        let coincideNombre = platillo.nombre.toLowerCase().includes(texto);
        let coincideCategoria = categoria === "todos" || platillo.categoria === categoria;

        if (coincideNombre && coincideCategoria) {
            listaFiltrada.push(platillo);
        }
    }

    mostrarMenu(listaFiltrada);
}

// Agregar un objeto platillo al arreglo de pedido
function agregarAlPedido(idPlatillo) {
    for (let platillo of menuRestaurante) {
        if (platillo.id === idPlatillo) {
            pedido.push(platillo);
            break;
        }
    }
    actualizarPedido();
}

// Actualizar tabla y calcular la suma total
function actualizarPedido() {
    tablaPedido.innerHTML = "";

    if (pedido.length === 0) {
        tablaPedido.innerHTML = `
            <tr>
                <td colspan="3" class="text-center text-muted py-4">No has añadido productos a tu pedido.</td>
            </tr>
        `;
        precioTotal.textContent = "$0 COP";
        return;
    }

    let sumaTotal = 0;

    for (let item of pedido) {
        sumaTotal += item.precio;

        tablaPedido.innerHTML += `
            <tr>
                <td class="fw-semibold">${item.nombre}</td>
                <td><span class="badge bg-secondary">${item.categoria}</span></td>
                <td>${item.precioFormateado()}</td>
            </tr>
        `;
    }

    precioTotal.textContent = `$${sumaTotal.toLocaleString('es-CO')} COP`;
}

inputBuscador.addEventListener("input", filtrarMenu);
selectCategoria.addEventListener("change", filtrarMenu);

btnVaciar.addEventListener("click", function() {
    pedido = [];
    actualizarPedido();
});

mostrarMenu(menuRestaurante);