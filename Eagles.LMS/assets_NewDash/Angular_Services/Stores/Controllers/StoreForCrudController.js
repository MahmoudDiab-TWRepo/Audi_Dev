angular.module("InventoryApp_Controllers").controller("storeCtrl",
    ["$scope", "NotificationServices", "storesFactory", "DataTableServices", "branchsFactory",
        (s, n, storesFactory, dT, branchFactory) => {


            //get all branchs
            branchFactory.GetAllBranchs().then(function success(response) {
                s.Branchs = response.data;
                s.Branch_dll = response.data[0];


            }, function error(error) {
                let errorMessage = manageAjaxError(error);
                n.pushNotification("danger", errorMessage);
            });

            //get all mainStore
            let GetMainStores=() => {
                storesFactory.GetAllStore('main').then(function success(response) {
                   
                    s.MainStores = response.data;
                    s.MainStore_dll = response.data[0];
                    console.log(s.MainStores);

                }, function error(error) {
                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
                });

            }
            GetMainStores();

            if (bind() != undefined) {
                storesFactory.GetById(bind()).then(function success(response) {
                    s.StoreCode = response.data.StoreCode;
                    s.StoreName = response.data.StoreName;
                    s.IsChildStore = response.data.IsChildStore;


                    var barnch = s.Branchs.filter(arr => {

                        if (arr.Id == response.data.BranchId) {

                            return arr;
                        }
                    });
                    s.Branch_dll = barnch[0];

                    if (s.IsChildStore) {
                        var mainStore = s.MainStores.filter(arr => {

                            if (arr.Id == response.data.MainStoreId) {

                                return arr;
                            }
                        });
                        s.MainStore_dll = mainStore[0];
                    }


                }, function error(error) {

                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
                });
            }







            function Clear() {
                s.StoreCode = "";
                s.StoreName = "";
                s.IsChildStore = false;
                GetMainStores();



            }

            // validate form
            function validateForm(isEditMode = false) {

                let flag = false;
                if (s.StoreCode == "" || typeof (s.StoreCode) == 'undefined') {

                    n.pushNotification("danger", "Please Enter The Store Code");
                }
                else if (s.StoreName == "" || typeof (s.StoreName) == 'undefined') {
                    n.pushNotification("danger", "Please Enter The Store Name");
                }



                else if (s.IsChildStore == true && (s.MainStore_dll == null || s.MainStore_dll == "" || typeof (s.MainStore_dll) == 'undefined')) {
                    n.pushNotification("danger", "Please choose Main Store");
                }
                else
                    flag = true;

                return flag;
            }

            s.CreateStore = () => {

                if (validateForm()) {
                    let store = {
                        StoreCode: s.StoreCode,
                        StoreName: s.StoreName,

                        IsChildStore: s.IsChildStore,
                        BranchId: s.Branch_dll.Id
                    };
                    if (s.MainStore_dll != null) {
                        store.MainStoreId = s.MainStore_dll.Id;
                    }
                    // get all safes    
                    storesFactory.CreateStore(store).then(function success(response) {
                        n.pushNotification("success", "Added Successfully");
                        Clear();
                    }, function error(error) {

                        let errorMessage = manageAjaxError(error);
                        n.pushNotification("danger", errorMessage);
                    });
                }
            };
            function bind() {
                var usid = $("#StoreID").val();
                return usid;
            }
            s.EditStore = () => {

                if (validateForm(isEditMode = true)) {
                    let store = {
                        StoreCode: s.StoreCode,
                        StoreName: s.StoreName,
                        IsChildStore: s.IsChildStore,
                        BranchId: s.Branch_dll.Id,
                        Id: bind()
                    };
                    if (s.MainStore_dll != null) {
                        store.MainStoreId = s.MainStore_dll.Id;
                    }


                    storesFactory.UpdateStore(store).then(function success(response) {
                        n.pushNotification("success", "Updated Successfully");

                    }, function error(error) {

                        let errorMessage = manageAjaxError(error);
                        n.pushNotification("danger", errorMessage);


                    });
                }
            };

        }]);


