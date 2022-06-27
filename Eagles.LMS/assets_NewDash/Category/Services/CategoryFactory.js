angular.module("InventoryApp_Services").factory("categoryFactory", ($http) => {
    var addCategory = (obj) => {
        return $http({
            method: "POST",
            url: '/Admission/Categories/Create',
            data: JSON.stringify(obj)
        });
    };
    var editCategory = (obj) => {
        return $http({
            method: "POST",
            url: '/Admission/Categories/Edit',
            data: JSON.stringify(obj)
        });
    };
    var deleteCategory = (categoryId) => {
        return $http({
            method: "POST",
            url: '/Admission/Categories/Delete/' + categoryId + ''
        });
    };
    var deleteProperty = (propertyId) => {
        return $http({
            method: "POST",
            url: '/Admission/Categories/DeleteProperty/' + propertyId + ''
        });
    };
    

    var getAll = () => {
        return $http({
            method: "GET",
            url: '/api/ApiCategories/GetAll'
        });
    };

    var getAllCategoryProperties = (categoryId) => {
        return $http({
            method: "GET",
            url: '/api/ApiCategories/' + categoryId+'/GetAllProperies'
        });
    };
    var getById = (Storeid) => {
        return $http({
            method: "GET",
            url: "/api/ApiStores/" + Storeid + "/Get",
        });
    };

    return {
        CreateCategory: addCategory,
        GetAllCategories: getAll,
        GetAllCategoryProperties: getAllCategoryProperties,
        DeleteProperty: deleteProperty,
        RemoveCategory: deleteCategory,
        GetById: getById,
        UpdateCategory: editCategory
    };
});