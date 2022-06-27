angular.module("InventoryApp_Controllers").controller("itemCtrl",
    ["$scope", "NotificationServices", "ItemsFactory", "DataTableServices", "storesFactory",
        (s, n, itemsFactory, dT, storesFactory) => {
            s.showItems = false;

            storesFactory.GetAllStore("Main").then(function success(response) {
                let data = response.data;
                data.push({
                    Id: "", StoreName: "All"
                });
                s.MainStore_dll = {
                    Id: "", StoreName: "All"
                };
                s.MainStores = data;
                s.getSubStore();

            }, function error(error) {
                let errorMessage = manageAjaxError(error);
                n.pushNotification("danger", errorMessage);
            });
            s.subStoreChange = () => {
                s.Items = [];


            }
            s.getSubStore = () => {
                s.Items = [];


                let mainStoreId = s.MainStore_dll.Id;
                if (mainStoreId == null || mainStoreId == "") {
                    storesFactory.GetAllStore("Sub").then(function success(response) {
                        let data = response.data;

                        data.push({
                            Id: "", StoreName: "All"
                        });
                        s.SubStores = data;

                        s.SubStore_dll = {
                            Id: "", StoreName: "All"
                        };
                    }, function error(error) {
                        let errorMessage = manageAjaxError(error);
                        n.pushNotification("danger", errorMessage);
                    });
                } else {
                    //get all Sub Store
                    storesFactory.GetSubStore(mainStoreId).then(function success(response) {


                        let data = response.data;

                        data.push({
                            Id: "", StoreName: "All"
                        });
                        s.SubStores = data;

                        s.SubStore_dll = {
                            Id: "", StoreName: "All"
                        };
                    }
                        , function error(error) {
                            let errorMessage = manageAjaxError(error);
                            n.pushNotification("danger", errorMessage);
                        });
                }
            }

            s.getItem = () => {
                s.Items = [];
                s.manageSelectAll = false;
                itemsFactory.GetAllItem('Pending', s.MainStore_dll.Id, s.SubStore_dll.Id).then(function success(response) {

                    s.showItems = true;
                    s.Items = response.data;
                    dT.pushDataTable("#table_Items");
                }, function error(error) {
                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
                });
            }
            s.openCategoryProperty = (item) => {

                itemsFactory.GetAllItemPropertiesValues(item.Id).then(function success(response) {
                    s.CategoryPropeties = response.data.Properts;
                    $("#modalCategoryValuesProperties").modal();
                }, function error(error) {
                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
                });

            }

            s.changeItemsSelect = (e) => {

                s.Items.map(function (key) {
                    if (key.SubStoreId != null) {
                        key.IsChecked = s.manageSelectAll;
                    }
                    return key;
                });


            }
            s.changeItemsStatus = (status) => {

                //let items = s.Items.filter(function (key) {
                //    return key.IsChecked;
                //});
                const items = s.Items.reduce((a, o) => (o.IsChecked && a.push(o.Id), a), []);
                if (items.length == 0) {
                    n.pushNotification("danger", 'At least one item must be selected');
                } else {
                    bootbox.prompt({
                        className: "largeInput",
                        size: "Large",
                        title: "confirm " + status + " and any notes?",
                        callback: function (notes) {
                            if (notes != null) {

                                itemsFactory.ChangeItemsStatus({
                                    Items: items,
                                    Notes: notes,
                                    ItemStatus: status
                                }).then(function success(response) {
                                    n.pushNotification("success", "Updated Successfully");
                                    s.getItem();
                                }, function error(error) {

                                    let errorMessage = manageAjaxError(error);
                                    n.pushNotification("danger", errorMessage);


                                });
                            }
                        }
                    });
                }
            }



        }]);