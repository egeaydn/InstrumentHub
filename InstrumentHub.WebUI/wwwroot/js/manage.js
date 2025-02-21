$(document).ready(function () {
    // Formu yumuşakça görünür hale getirme (fade-in)
    $('.col-md-3').hide().fadeIn(1000);

    // Form gönderiminde butonu devre dışı bırak ve mesaj göster
    $('form').on('submit', function () {
        var $submitButton = $(this).find('input[type="submit"]');
        $submitButton.prop('disabled', true);
        $submitButton.val('İşlem Devam Ediyor...');
    });

    // Input elemanlarına odaklanma sırasında renk geçişi
    $('.form-control').on('focus', function () {
        $(this).animate({ borderColor: '#ff4c4c' }, 300);
    }).on('blur', function () {
        $(this).animate({ borderColor: '#ccc' }, 300);
    });

    // jQuery Validation hata durumunda "shake" animasyonu uygulama
    $('form').validate({
        errorPlacement: function (error, element) {
            error.insertAfter(element);
        },
        invalidHandler: function (event, validator) {
            // Hatalı alanları bul ve shake sınıfı ekle
            $.each(validator.errorList, function (index, errorItem) {
                $(errorItem.element).closest('.form-group').addClass('shake');
                // Animasyon bitince shake sınıfını kaldır
                setTimeout(function () {
                    $(errorItem.element).closest('.form-group').removeClass('shake');
                }, 500);
            });
        }
    });
});
