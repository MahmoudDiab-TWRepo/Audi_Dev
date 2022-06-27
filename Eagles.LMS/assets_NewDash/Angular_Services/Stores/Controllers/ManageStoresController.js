angular.module("InventoryApp_Controllers").
    controller("StoreCtrl", ["$scope", "NotificationServices", "storesFactory", "DataTableServices", (s, n, f, dT) => {
         
        
        function bind() {
            return $("#groupId").val();

        }

        // get all stores
        f.GetAllStoresByTree(bind()).then(function success(response) {

            let StoreArr = [],
                StoreTree = response.data.Tree;
            s.txt_groupName = response.data.GroupName;
            s.groupId = response.data.GroupId;
            for (var i = 0; i < StoreTree.length; i++) {
                if (StoreTree[i].Id !== 1) {
                    StoreArr.push({ "id": "" + StoreTree[i].Id + "", "parent": "#", "text": "" + StoreTree[i].ParentName + "", "state": { "selected": false } });
                }
                for (var j = 0; j < StoreTree[i].SubStore.length; j++) {

                    if (StoreTree[i].Id !== 1)
                        StoreArr.push({ "id": "" + StoreTree[i].SubStore[j].Id + "", "parent": "" + StoreTree[i].Id + "", "text": "" + StoreTree[i].SubStore[j].StoreName + "", "state": { "selected": StoreTree[i].SubStore[j].IsChecked } });
                    else
                        StoreArr.push({ "id": "" + StoreTree[i].SubStore[j].Id + "", "parent": "#", "text": "" + StoreTree[i].SubStore[j].StoreName + "", "state": { "selected": StoreTree[i].SubStore[j].IsChecked } });

                }
            }
            $('#jstree').jstree({
                "types": {
                    "default": { "icon": "fa fa-folder kt-font-warning" }
                },
                "plugins": ["checkbox", "types"],
                'core': {
                    'data': StoreArr
                }
            });


        }, function error(error) {
            let errorMessage = manageAjaxError(error);
            n.pushNotification("danger", errorMessage);
        });

       
        //update
        s.Update = () => {
            if (validateForm()) {
                var storesArray = $('#jstree').jstree("get_selected");
                f.ManageStores({ GroupId: bind(), SubStores: storesArray }).then(function success(response) {
                    n.pushNotification("success", "Updated Successfully");
                }, function error(error) {
                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
                });
            }
        };

        // validate form
        function validateForm() {
            let flag = false;
             if ($('#jstree').jstree("get_selected").length == 0) {
                n.pushNotification("danger", "Please specify permissions!!");
            }
            else
                flag = true;
            return flag;
        }
        function clear() {
            $('#jstree').jstree(true).deselect_all();
        }

    }]);