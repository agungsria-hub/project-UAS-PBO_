// =========================
// DataTables
// =========================
$(function () {

    if ($('#dataTable').length) {

        $('#dataTable').DataTable({

            destroy: true,

            responsive: true,

            pageLength: 10,

            columnDefs: [
                {
                    targets: 0,
                    orderable: false,
                    searchable: false
                }
            ],

            language: {

                search: "Cari :",

                lengthMenu: "Tampilkan _MENU_ data",

                zeroRecords: "Data tidak ditemukan",

                info: "Menampilkan _START_ - _END_ dari _TOTAL_ data",

                infoEmpty: "Tidak ada data",

                paginate: {

                    first: "Awal",

                    last: "Akhir",

                    next: "→",

                    previous: "←"

                }

            }

        });

    }

});


// =========================
// SweetAlert Delete
// =========================

function confirmDelete(e, element) {

    e.preventDefault();

    Swal.fire({

        title: 'Yakin?',

        text: 'Data akan dihapus.',

        icon: 'warning',

        showCancelButton: true,

        confirmButtonText: 'Ya',

        cancelButtonText: 'Batal',

        confirmButtonColor: '#dc3545'

    }).then((result) => {

        if (result.isConfirmed) {

            window.location.href = element.href;

        }

    });

    return false;

}