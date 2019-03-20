<template>
<div id="app">
   <v-app id="inspire">
     <v-container >
       <v-layout align-center justify-center row fill-height>
         <v-flex table>
  <v-data-table
    :headers="headers"
    :items="users"
    class="elevation-1"
  >
    <template slot="items" slot-scope="props">
      <td class="text-xs-left">{{ props.item.Id }}</td>
      <td class="text-xs-left">{{ props.item.UserName }}</td>
      <td class="text-xs-left">{{ props.item.FirstName }}</td>
      <td class="text-xs-left">{{ props.item.LastName }}</td>
      <td class="text-xs-left">{{ props.item.Email }}</td>
      <td class="text-xs-;fef">{{ props.item.BirthDate }}</td>
      <v-btn v-on:click="deleteUser(props.item.Id)" >Delete</v-btn>
      <v-btn v-on:click="showEditModal" >Edit</v-btn>
    </template>
  </v-data-table>
         </v-flex>
         <v-flex  >
    <v-btn v-on:click="showNewUserModal">Add</v-btn>
         </v-flex>
       </v-layout>

     </v-container>
   </v-app>
       <EditModal v-bind:username = "UserName"
      v-show="isEditModalVisible"
      
      @close="closeEditModal"
    />
    <NewUserModal v-bind:username = "UserName"
      v-show="isNewUserModalVisible"
      
      @close="closeNewUserModal"
    />
    
    


</div>




</template>



<script>
/*global console*/ /* eslint no-console: "off" */
import EditModal from '@/components/EditModal.vue';
import NewUserModal from '@/components/CreateUserModal.vue';
  export default {
  components: {
      EditModal,
      NewUserModal
  },
    data () {
      return {
        headers: [
          { text: 'id', value: 'Id' },
          {
            text: 'Username',
            align: 'left',
            sortable: false,
            value: 'UserName'
          },
          { text: 'First Name', value: 'FirstName' },
          { text: 'Last Name', value: 'LastName' },
          { text: 'Email', value: 'Email' },
          { text: 'Date Of Birth', value: 'BirthDate' },
          
        ],
        users: [
          {
            UserName: 'Trong Nguyen',
            FirstName: 'Trong',
            LastName: 'Nguyen',
            Email: 'nguyentrong56@gmail.com',
            BirthDate: '10-16-1990',
          }
       
        ],
        isEditModalVisible: false,
        isNewUserModalVisible: false,
        response:"",
        }},
        
        created ()
        {
          this.fetchUsers();
        },
        watch: {
          '$route': 'fetchUsers',
           response: 'fetchUsers'
          
        },

        methods: {
          showEditModal() {
            this.isEditModalVisible = true;
          },
          closeEditModal() {
            this.isEditModalVisible =false;
          },
          showNewUserModal() {
            this.isNewUserModalVisible = true;
          },
          closeNewUserModal() {
            this.isNewUserModalVisible =false;
          },
          
          fetchUsers () {
          this.axios.get('http://localhost:60500/api/usermanager', {
            headers: {'Content-Type' : 'application/Json'}
            
          }
          )
          .then((response) => {
            this.users = response.data;
            console.log(response.data);
            })
            .catch((error) => {
              console.log(error);
            })

        },
        deleteUser(id){
          this.axios.delete('http://localhost:60500/api/usermanager/'+ id)
          .then((response) => {this.response = response;})
          }
        }
      }
    
  
</script>