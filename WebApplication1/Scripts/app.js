$(function () {
    var app = new Vue({
        el: '#app',
        data: {
            clients: [],
            selectedClient: null,
            recentOrders: []
        },
        methods: {
            showRecentOrders: function (client) {
                this.selectedClient = client;
                // GET recentOrdersFromServer here
           
                // Use this.recentOrders.push(recentOrdersFromServer) to update ViewModel

            }
        },
        mounted: function () {
            // GET clientsFromServer here
            var self = this;
            $.ajax({
                url: '/api/Clients',
                method: 'GET',
                success: function (data) {
                    self.clients = data;     
                },
                error: function (error) {
                    console.log(error);
                }
            });
            // Use this.clients.push(clientsFromServer) to update ViewModel

        }
    })
});
