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
      <td class="text-xs-right">{{ props.item.Id }}</td>
      <td class="text-xs-right">{{ props.item.UserName }}</td>
      <td class="text-xs-right">{{ props.item.FirstName }}</td>
      <td class="text-xs-right">{{ props.item.LastName }}</td>
      <td class="text-xs-right">{{ props.item.Email }}</td>
      <td class="text-xs-right">{{ props.item.BirthDate }}</td>
      <v-btn v-on:click="deleteUser(props.item.Id)" color="Add">Delete</v-btn>
      <v-btn v-on:click="switchComponent('UserForm')" color="Add">Edit</v-btn>
    </template>
  </v-data-table>
         </v-flex>
         <v-flex  align-self-center>
    <v-btn color="Add">Add</v-btn>
         </v-flex>
       </v-layout>

     </v-container>
   </v-app>
</div>




</template>



<script>
/*global console*/ /* eslint no-console: "off" */
import { bus } from '../router/index.js';
  export default {
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
        currentComponent: 'UserList',
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
          },
        switchComponent(comp){
          bus.$emit('switchComp', comp);
        }   
        }
      }
    
  
</script>