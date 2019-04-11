jQuery.extend({
    loadPartial: function(options) {
        var defaults = {
            link: '.sidebar-menu li > a',
            container: '#main-Content'
        };
        defaults = $.extend(defaults, options);
        mainContent = $(defaults.container);

        $(defaults.link).each(alink);

        function alink(idx, link) {
            if ($(link).attr("onclick")) return;
            if (link.href.indexOf('###') >= 0) return;
            if (link.href.indexOf('#') >= 0) {
                var addr1 = link.href.split('#');
                var addr2 = location.href.split('#');
                if (addr1[0] == addr2[0]) return;
            }

            $(link).click(function (e) {
                var origin = window.location.origin;  
                var route = link.href.replace(origin,""); 
                e.preventDefault();

                window.history.pushState({}, 0, origin + "/#" + route);
                $("#contextFrame").attr("src", link.href); 
            });
        }
    }
});