// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {

    $('#students').DataTable({
        processing: true,
        ordering: true,
        paging: true,
        searching: true,
        ajax: {
            "url": "https://localhost:7058/api/Datatable/Students",
            "type": "GET",
            "datatype": "json"
        },
        columnDefs: [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        columns: [
            { data: "studentID" },
            { data: "firstname", name: "Firstname", autoWidth: true },
            { data: "lastname", name: "Lastname", autoWidth: true },
            {
                data: 'promotion',
                name: 'Promotion',
                render: function (data, type, row) {
                    return type === "display" || type === "sort" || type === "filter" ? data.name : data;
                }, autoWidth: true
            },
            {
                data: 'group',
                name: 'Groupe',
                render: function (data, type, row) {
                    return type === "display" || type === "sort" || type === "filter" ? data.name : data;
                }, autoWidth: true
            }
        ]
    });

    $('#devices').DataTable({
        processing: true,
        ordering: true,
        paging: true,
        searching: true,
        ajax: {
            "url": "https://localhost:7058/api/Datatable/Devices",
            "type": "GET",
            "datatype": "json"
        },
        columnDefs: [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        columns: [
            { data: "deviceID" },
            { data: "macAddress", name: "Adresse MAC", autoWidth: true },
            {
                data: 'isActive',
                name: 'Activation',
                render: function (data, type, row) {
                    if (data === true && type === "display") {
                        return " <div class='alert alert-primary text-center' role='alert'>Actif</div> ";
                    }
                    if (data === false && type === "display") {
                        return " <div class='alert alert-danger text-center' role='alert'>Inactif</div> ";
                    }
                    if (data === true && type === "filter") {
                        return 'actif';
                    }
                    if (data === false && type === "filter") {
                        return 'inactif';
                    }
                    if (type === "sort") {
                        return data;
                    }
                    return data;
                }, autoWidth: true
            },
            {
                data: 'registeredAt',
                name: 'Date d\'enregistrement',
                render: function (data, type, row) {
                    var date = new Date(data);
                    var formattedDate = date.toLocaleString('fr-FR', {
                        year: 'numeric',
                        month: '2-digit',
                        day: '2-digit',
                        hour: '2-digit',
                        minute: '2-digit',
                        second: '2-digit'
                    });
                    return type === "display" || type === "filter" ? formattedDate : data;
                }, autoWidth: true
            },
            {
                data: 'student',
                name: 'Étudiant',
                render: function (data, type, row) {
                    return type === "display" || type === "sort" || type === "filter" ? data.firstname + ' ' + data.lastname : data;
                }, autoWidth: true
            }
        ]
    });

    $('#signatures').DataTable({
        processing: true,
        ordering: true,
        paging: true,
        searching: true,
        ajax: {
            "url": "https://localhost:7058/api/Datatable/Signatures",
            "type": "GET",
            "datatype": "json"
        },
        columnDefs: [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        columns: [
            { data: 'signatureID' },
            {
                data: 'createdAt',
                name: 'Date de création',
                render: function (data, type, row) {
                    var date = new Date(data);
                    var formattedDate = date.toLocaleString('fr-FR', {
                        year: 'numeric',
                        month: '2-digit',
                        day: '2-digit',
                        hour: '2-digit',
                        minute: '2-digit',
                        second: '2-digit'
                    });
                    return type === "display" || type === "filter" ? formattedDate : data;
                }, autoWidth: true
            },
            {
                data: 'isPresent',
                name: 'Présence',
                render: function (data, type, row) {
                    if (data === true && type === "display") {
                        return " <div class='alert alert-success text-center' role='alert'>Présence</div> ";
                    }
                    if (data === false && type === "display") {
                        return " <div class='alert alert-warning text-center' role='alert'>Absence</div> ";
                    }
                    if (data === true && type === "filter") {
                        return 'présence';
                    }
                    if (data === false && type === "filter") {
                        return 'absence';
                    }
                    if (type === "sort") {
                        return data;
                    }
                    return data;
                }, autoWidth: true
            },
            {
                data: 'student',
                name: 'Elève',
                render: function (data, type, row) {
                    return type === "display" || type === "sort" || type === "filter" ? data.firstname + ' ' + data.lastname : data;
                }, autoWidth: true
            },
            {
                data: 'student',
                name: 'Promotion',
                render: function (data, type, row) {
                    return type === "display" || type === "sort" || type === "filter" ? data.promotion.name : data;
                }, autoWidth: true
            },
            {
                data: 'student',
                name: 'Groupe',
                render: function (data, type, row) {
                    return type === "display" || type === "sort" || type === "filter" ? data.group.name : data;
                }, autoWidth: true
            }
        ]
    });

    var studentsTable = $('#students').DataTable();
    var devicesTable = $('#devices').DataTable();

    $('#students tbody').on('click', 'tr', function () {
        var data = studentsTable.row(this).data();
        window.location.href = detailsStudentUrl.replace('__id__', data.studentID);
    });

    $('#devices tbody').on('click', 'tr', function () {
        var data = devicesTable.row(this).data();
        window.location.href = detailsDeviceUrl.replace('__id__', data.deviceID);
    });

    const url = window.location.pathname;
    const id = url.substring(url.lastIndexOf('/') + 1);

    $('#studentSignatures').DataTable({
        processing: true,
        ordering: true,
        paging: true,
        searching: true,
        ajax: {
            "url": "https://localhost:7058/api/Datatable/Student/" + id,
            "type": "GET",
            "datatype": "json"
        },
        columnDefs: [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        columns: [
            { data: 'signatureID' },
            {
                data: 'createdAt',
                name: 'Date de création',
                render: function (data, type, row) {
                    var date = new Date(data);
                    var formattedDate = date.toLocaleString('fr-FR', {
                        year: 'numeric',
                        month: '2-digit',
                        day: '2-digit',
                        hour: '2-digit',
                        minute: '2-digit',
                        second: '2-digit'
                    });
                    return type === "display" || type === "filter" ? formattedDate : data;
                }, autoWidth: true
            },
            {
                data: 'isPresent',
                name: 'Présence',
                render: function (data, type, row) {
                    if (data === true && type === "display") {
                        return " <div class='alert alert-success text-center' role='alert'>Présence</div> ";
                    }
                    if (data === false && type === "display") {
                        return " <div class='alert alert-warning text-center' role='alert'>Absence</div> ";
                    }
                    if (data === true && type === "filter") {
                        return 'présence';
                    }
                    if (data === false && type === "filter") {
                        return 'absence';
                    }
                    if (type === "sort") {
                        return data;
                    }
                    return data;
                }, autoWidth: true
            }
        ]
    });

});