var dataTable;

$(document).ready(function () {
    loadDatatable();
});

function loadDatatable() {
    dataTable = $("#tblDatos").DataTable({
        "ajax": {                                     
            "url": "/Sells/ObtenerTodos",    
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "customer.name", "width": "20%" },
            { "data": "fechaOrden", "width": "20%" },
            { "data": "totalOrden", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `    
                        <div class="text-center">
                            <a href="/Sells/InsertEditDetails/${data}" class="btn btn-success text-white" style="cursor:pointer">
                            <i class="fas fa-edit"></i> Ver/Añadir Detalles a esta venta</a>
                            <a onclick=Delete("/Sells/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                            <i class="fas fa-trash"></i></a>
                        </div>
                        `;
                }, "width": "30%"
            }

        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}
function Delete(url) {

    swal({
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }

            });
        }
    });
}

