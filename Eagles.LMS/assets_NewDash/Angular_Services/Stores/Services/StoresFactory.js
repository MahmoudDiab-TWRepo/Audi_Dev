angular.module("InventoryApp_Services").factory("storesFactory", ($http) => {
    var addStores = (obj) => {
        return $http({
            method: "POST",
            url: '/Admission/Stores/Create',
            data: JSON.stringify(obj)
        });
    };
    var editStores = (obj) => {
        return $http({
            method: "POST",
            url: '/Admission/Stores/Edit',
            data: JSON.stringify(obj)
        });
    };

    var manageStores = (obj) => {
        return $http({
            method: "POST",
            url: '/Admission/Groups/ManageStores',
            data: JSON.stringify(obj)
        });
    };

    
    var deleteStore = (StoreId) => {
        return $http({
            method: "POST",
            url: '/Admission/Stores/Delete/' + StoreId + ''
        });
    };


    var getAll = (type) => {
        return $http({
            method: "GET",
            url: '/api/ApiStores/GetAll?Type=' + type + ''
        });
    };
    var GetAllStoreForSearch = (type) => {
        return $http({
            method: "GET",
            url: '/api/ApiStores/GetAll?Type=' + type + ''
        });
    };

    var getAllStores = () => {
        return $http({
            method: "GET",
            url: '/api/ApiStores/GetAllStores'
        });
    };
    var getById = (Storeid) => {
        return $http({
            method: "GET",
            url: "/api/ApiStores/" + Storeid + "/Get",
        });
    };
    var getSubStore = (mainStoreId) => {
 
        return $http({
            method: "GET",
            url: "/api/ApiStores/MainStore/" + mainStoreId + "/GetSubStore",
        });
    };
    var getAllStoresByTree = (groupID) => {

        return $http({
            method: "GET",
            url: "/api/ApiStores/" + groupID + "/GetAllStoresByTree",
        });
    };

    
    var getSubStoreForSearch = (mainStoreId) => {

        return $http({
            method: "GET",
            url: "/api/ApiStores/MainStore/" + mainStoreId + "/GetSubStore",
        });
    };

    

    var getAllCategoriesddl = () => {
        return $http({
            method: "GET",
            url: '/api/ApiCategories/GetAllCategory'
        });
    };

    var getBranchs = (groupId) => {
        return $http({
            method: "GET",
            url: '/api/ApiBranchs'
        });
    };

    var getStoresOfThisBranch = (branchId) => {

        return $http({
            method: "GET",
            url: "/api/ApiStores/MainStore/" + branchId + "/getStoresOfBranch",
        });
    };
    var getAllForSearch = (type = null, branchId = "", mainStoreId = "", subStoreId = "") => {
        let _url = '/api/ApiItems/getAllForSearch?';
        if (type != null && typeof (type) != undefined && type != "all")
            _url += "Status=" + type + "&";

        _url += "BranchId=" + branchId + "&mainStoreId=" + mainStoreId + "&subStoreId=" + subStoreId + "";
        return $http({
            method: "GET",
            url: _url
        });
    };
    return {
        CreateStore: addStores,
        GetAllStore: getAll,
        getAllStores: getAllStores,
        getAllCategories: getAllCategoriesddl,
        RemoveStore: deleteStore,
        GetById: getById,
        getAllForSearch: getAllForSearch,
        UpdateStore: editStores,
        GetSubStore: getSubStore,
        GetAllBranchs: getBranchs,
        GetAllStoreForSearch: GetAllStoreForSearch,
        getSubStoreForSearch: getSubStoreForSearch,
        getStoresOfThisBranch: getStoresOfThisBranch,
        GetAllStoresByTree: getAllStoresByTree,
        ManageStores: manageStores

    };
});