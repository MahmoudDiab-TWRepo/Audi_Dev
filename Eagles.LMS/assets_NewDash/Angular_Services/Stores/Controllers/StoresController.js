angular.module("InventoryApp_Controllers").controller("storeCtrl",
    ["$scope", "NotificationServices",  "storesFactory", "DataTableServices",
        (s, n, storesFactory, dT) => {
 
            // get all Stores
            storesFactory.GetAllStore().then(function success(response) {

                s.Stores = response.data;
                dT.pushDataTable("#table_Stores");
            }, function error(error) {
                let errorMessage = manageAjaxError(error);
                n.pushNotification("danger", errorMessage);
            });

            // remove Stores
            s.Remove = (storeId, index, e) => {
                e.preventDefault();
                bootbox.confirm('Confirm Delete Operation', function (flag) {
                    if (flag) {
                        storesFactory.RemoveStore(storeId).then(function success(response) {
                            s.Stores.splice(index, 1);
                            n.pushNotification("success", "Successfully Deleted");
                        }, function error(error) {

                            let errorMessage = manageAjaxError(error);
                            n.pushNotification("danger", errorMessage);
                        });
                    }
                });
            };
            s.ddl_storeType = "all";
            s.storeFilter = () => {
                let type = s.ddl_storeType;
                storesFactory.GetAllStore(type).then(function success(response) {
                    s.Stores = response.data;
                    //dT.pushDataTable("#table_Stores");
                }, function error(error) {
                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
                });
            }
       

        
        }]);