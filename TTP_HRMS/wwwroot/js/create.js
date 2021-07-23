$('#menu_paymentprocess').addClass('active');

$(document).ready(function () {
  $('#File').change(function (event) {
    var selectedFile = event.target.files[0];
    var reader = new FileReader();

    var img = $('#image');
    img.title = selectedFile.name;

    reader.onload = function (event) {
      img.attr('src', event.target.result);
    };

    reader.readAsDataURL(selectedFile);
  });

  if (error) {
    swal('Error', error, 'error');
  }
});

$(function () {
  $('#form').validate({
    ignore: [],
    rules: {
      'File': {
        required: true,
        minFileSize: 1048576
      }
    },
    highlight: function (input) {
      $(input).parents('.form-line').addClass('error');
    },
    unhighlight: function (input) {
      $(input).parents('.form-line').removeClass('error');
    },
    errorPlacement: function (error, element) {
      $(element).parents('.form-group').append(error);
    }
  });

  $.validator.addMethod('minFileSize', function (value, element, param) {
    return element.files[0].size < param;
  }, 'File is too big. File size must be less than 1mb.'
  );
});

function selectImage() {
  $('#File').trigger('click');
}

function confirmSubmit() {
  if (!$('#form').valid()) {
    return;
  }

  swal({
    title: "Confirmation",
    text: "Are you sure! you want to upload this result file?",
    type: "warning",
    allowOutsideClick: true,
    showCancelButton: true,
    confirmButtonColor: "#2196F3",
    confirmButtonText: "Yes, upload it!",
    closeOnConfirm: false
  }, function () {
    $('#form').submit();
  });
}