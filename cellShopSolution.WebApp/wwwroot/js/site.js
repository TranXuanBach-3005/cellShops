var SiteController = function () {
    this.initialize = function () {
        regsiterEvents();
        loadData();
    }
    function loadData() {
        const culture = $('#hidCulture').val();
        $.ajax({
            type: "GET",
            url: "/" + culture + '/Cart/GetlistItmes',
            success: function (res) {
                $('#lbl_nuber_items_header').text(res.length);
            }
        });
    }
    function regsiterEvents() {
        $('body').on('click', '.btn-add-cart', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            const culture = $('#hidCulture').val();
            $.ajax({
                type: "POST",
                url: "/" + culture + '/Cart/AddToCart',
                data: {
                    id: id,
                    languageId: culture
                },
                success: function (res) {
                    $('#lbl_nuber_items_header').text(res.length);
                },
                error: function (err) {
                    console.log(err);
                }
            });
        });
    }
}