<template>
  <v-data-table
    :headers="headers"
    :items="users"
    class="elevation-1"
  >
    <template v-slot:items="props">
      <td>{{ props.item.name }}</td>
      <td class="text-xs-right">{{ props.item.username }}</td>
      <td class="text-xs-right">{{ props.item.firstName }}</td>
      <td class="text-xs-right">{{ props.item.lastName }}</td>
      <td class="text-xs-right">{{ props.item.email }}</td>
      <td class="text-xs-right">{{ props.item.dob }}</td>
    </template>
  </v-data-table>
</template>

<script>
  export default {
    data () {
      return {
        headers: [
          {
            text: 'Username',
            align: 'left',
            sortable: false,
            value: 'username'
          },
          { text: 'First Name', value: 'firstName' },
          { text: 'Last Name', value: 'lastName' },
          { text: 'Email', value: 'email' },
          { text: 'Date Of Birth', value: 'dob' },
          
        ],
        users: [
          {
            username: 'Trong Nguyen',
            firstName: 'Trong',
            lastName: 'Nguyen',
            email: 'nguyentrong56@gmail.com',
            dob: '10-16-1990',
          },
       
        ]
      }
    }
  }
</script>