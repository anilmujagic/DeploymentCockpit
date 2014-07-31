var deploymentCockpit = deploymentCockpit || {};

deploymentCockpit.crudServiceFactory = (function () {
    var create = function (resource, restResourceName) {
        var url = "/api/" + restResourceName + "/:id";
        var _resource = resource(url, { id: "@id" }, { "update": { method: "PUT" } });

        return {
            getAll: function (urlParams) {
                return _resource.query(urlParams);
            },
            get: function (id) {
                return _resource.get({ id: id });
            },
            save: function (data, id) {
                if (id) {
                    return _resource.update({ id: id }, data);
                } else {
                    return _resource.save(data);
                }
            },
            delete: function (id) {
                return _resource.delete({ id: id });
            }
        };
    }

    return {
        create: create
    };
}());