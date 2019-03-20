<template>
  <div>
    <v-toolbar flat color="white">
      <v-toolbar-title>Error Logs</v-toolbar-title>
      <!-- <v-divider
        class="mx-2"
        inset
        vertical
      ></v-divider> -->
      <!-- <v-spacer></v-spacer> -->
      <v-dialog v-model="dialog" >
        <!-- <template v-slot:activator="{ on }">
          <v-btn color="primary" dark class="mb-2" v-on="on">New Item</v-btn>
        </template> -->
        <v-card>
          <!-- <v-card-title>
            <span class="headline">{{ formTitle }}</span>
          </v-card-title> -->

          <!-- <v-card-text> -->
            <!-- <v-container grid-list-md> -->
              <!-- <v-layout wrap> -->
                <!-- <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.name" label="Dessert name"></v-text-field>
                </v-flex> -->
                <!-- <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.calories" label="Calories"></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.fat" label="Fat (g)"></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.carbs" label="Carbs (g)"></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.protein" label="Protein (g)"></v-text-field>
                </v-flex> -->
              <!-- </v-layout> -->
            <!-- </v-container> -->
          <!-- </v-card-text> -->

          <!-- <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" flat @click="close">Cancel</v-btn>
            <v-btn color="blue darken-1" flat @click="save">Save</v-btn>
          </v-card-actions> -->
        </v-card>
      </v-dialog>
    </v-toolbar>
    <v-data-table
      :headers="headers"
      :items="errors"
      :pagination.sync="pagination" 
      :hide-actions= true
      :dark = false
      class="elevation-1"
    >
      <template v-slot:items="props">
        <td>{{ props.item.ID }}</td>
        <!-- <td class="text-xs-right">{{ props.item.calories }}</td>
        <td class="text-xs-right">{{ props.item.fat }}</td>
        <td class="text-xs-right">{{ props.item.carbs }}</td>
        <td class="text-xs-right">{{ props.item.protein }}</td> -->
        <td class="justify-center layout px-0">
          <!-- <v-icon
            small
            class="mr-2"
            @click="editItem(props.item)"
          >
            edit
          </v-icon> -->
          <v-icon
            small
            @click="deleteItem(props.item)"
          >
            delete
          </v-icon>
        </td>
      </template>
      <!-- <template v-slot:no-data>
        <v-btn color="primary" @click="initialize">Reset</v-btn>
      </template> -->
    </v-data-table>
  </div>
</template>





<script>
  import axios from 'axios'
  export default {
    data: () => ({
      dialog: false,
      headers: [
        {
          text: 'Errors',
          align: 'left',
          sortable: false,
          value: 'error.ID'
        }//,
        //,{ text: 'Date', value: 'date' },
        // { text: 'Fat (g)', value: 'fat' },
        // { text: 'Carbs (g)', value: 'carbs' },
        // { text: 'Protein (g)', value: 'protein' },
        // { text: 'Actions', value: 'name', sortable: false }
      ],
      errors: [],
      error: null, 
      //editedIndex: -1,
      // editedItem: {
      //   name: '',
      //   calories: 0,
      //   fat: 0,
      //   carbs: 0,
      //   protein: 0
      // },
      pagination: {
        expand: true,
        rowsPerPage: 1000
      },
       defaultItem: {
         ID: null,
         Date: null,
         Message: null,
         TargetSite: null,
         LineOfCode: null,
         BS: null
       }
    }),

    // computed: {
    //   formTitle () {
    //     return this.editedIndex === -1 ? 'New Item' : 'Edit Item'
    //   }
    // },

    // watch: {
    //   dialog (val) {
    //     val || this.close()
    //   }
    // },

    created () {
      this.initialize()
    },

    methods: {
      initialize () {
        //this.errors = [ 
        
          axios
            .get('http://localhost:60500/api/logging/geterrors', {
              headers: {
                'Content-Type': 'application/json'
              }
              //body: {
              //  'Content-Type': 'application/json'
              //}
            }) 
            .then(response => (this.error = [JSON.parse(response.data)]))
        //]
      },

      // editItem (item) {
      //   this.editedIndex = this.desserts.indexOf(item)
      //   this.editedItem = Object.assign({}, item)
      //   this.dialog = true
      // },

      deleteItem (item) {
        const index = this.desserts.indexOf(item)
        confirm('Are you sure you want to delete this item?') && this.desserts.splice(index, 1)
      },

      // close () {
      //   this.dialog = false
      //   setTimeout(() => {
      //     this.editedItem = Object.assign({}, this.defaultItem)
      //     this.editedIndex = -1
      //   }, 300)
      // },

      // save () {
      //   if (this.editedIndex > -1) {
      //     Object.assign(this.desserts[this.editedIndex], this.editedItem)
      //   } else {
      //     this.desserts.push(this.editedItem)
      //   }
      //   this.close()
      // }
    }
  }
</script>