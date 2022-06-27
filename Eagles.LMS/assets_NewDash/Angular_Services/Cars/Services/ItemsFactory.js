angular.module("InventoryApp_Services").factory("ItemsFactory", ($http) => {
    var addItems = (obj) => {
        return $http({
            method: "POST",
            url: '/Admission/Items/Create',
            data: JSON.stringify(obj)
        });
    };
    var editItems = (obj) => {
        return $http({
            method: "POST",
            url: '/Admission/Items/Edit',
            data: JSON.stringify(obj)
        });
    };
    var deleteItem = (ItemId) => {
        return $http({
            method: "POST",
            url: '/Admission/Items/Delete/' + ItemId + ''
        });
    };

    var changeStatus = (obj) => {
        console.log(obj);
        return $http({
            method: "POST",
            url: '/Admission/Items/ApprovalPendingItems',
            data: JSON.stringify(obj)
        });
    };
    var getAll = (type=null ,mainStoreId="", subStoreId="") => {
        let _url = '/api/ApiItems/GetAll?';
        if (type != null && typeof (type) != undefined && type != "all")
            _url += "Status=" + type + "&";
    
        _url += "mainStoreId=" + mainStoreId + "&subStoreId=" + subStoreId + "";
        return $http({
            method: "GET",
            url: _url
        });
    };

    var getAllForSearch = (type = null,branchId="", mainStoreId = "", subStoreId = "") => {
        let _url = '/api/ApiItems/getAllForSearch?';
        if (type != null && typeof (type) != undefined && type != "all")
            _url += "Status=" + type + "&";

        _url += "BranchId=" + branchId+ "&mainStoreId=" + mainStoreId + "&subStoreId=" + subStoreId + "";
        return $http({
            method: "GET",
            url: _url
        });
    };
    var getById = (id) => {
        return $http({
            method: "GET",
            url: "/api/ApiItems/" + id + "/Get",
        });
    };


    var CreateControls = (Id) => {
        return $http({
            method: "GET",
            url: "/api/ApiItems/" + Id + "/CreateControls",
        });
    };


    var LoadControls = (Id) => {
        return $http({
            method: "GET",
            url: "/api/ApiItems/" + Id + "/LoadControls",
        });
    };

    var GetAllItemPropertiesValues = (id) => {
        return $http({
            method: "GET",
            url: "/api/ApiItems/" + id + "/GetAllItemPropertiesValues",
        });
    };
    var getItemWithoutCondtion = (key) => {
        return $http({
            method: "GET",
            url: "/api/ApiItems/" + key + "/GetItemWithoutCondtion",
        });
    };
    var GetAllItemMovmentStatuses = (id) => {
        return $http({
            method: "GET",
            url: "/api/ApiItems/" + id + "/GetAllItemMovmentStatuses",
        });
    };
    var getItemEditHistoryies = (itemHistoryId) => {
        return $http({
            method: "GET",
            url: "/api/ApiItems/" + itemHistoryId + "/GetAllItemHistoriesForEditing",
        });
    };
    

    var LoadStores = (itemId) => {
        return $http({
            method: "GET",
            url: "/api/ApiItems/" + itemId + "/LoadStores",
        });
    };

    return {
     CreateItem: addItems,
        GetAllItem: getAll,
        CreateControls: CreateControls,
        LoadControls: LoadControls,
        GetAllItemPropertiesValues: GetAllItemPropertiesValues,
        GetAllItemMovmentStatuses: GetAllItemMovmentStatuses,
        RemoveItem: deleteItem,
        GetById: getById,
        UpdateItem: editItems,
        ChangeItemsStatus:changeStatus,
        LoadStores: LoadStores,
        getAllForSearch: getAllForSearch,
        GetItemEditHistoryies: getItemEditHistoryies,
        GetItemWithoutCondtion: getItemWithoutCondtion
    };
});