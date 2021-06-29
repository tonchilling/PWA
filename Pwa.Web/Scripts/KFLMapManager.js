

var KLFMap = {
    Init: function (mapIDName, options) {
        var map = L.map(mapIDName);

        if (map != null) {

            map.setView([13.75030, 100.29520], 17);
            L.tileLayer('http://{s}.google.com/vt/lyrs=p&x={x}&y={y}&z={z}', {
                maxZoom: 1500,
                subdomains: ['mt0', 'mt1', 'mt2', 'mt3']
            }).addTo(map);



       
            var drawControl = new L.Control.Draw(options);
            map.addControl(drawControl);


        }

        return map;
    },

}