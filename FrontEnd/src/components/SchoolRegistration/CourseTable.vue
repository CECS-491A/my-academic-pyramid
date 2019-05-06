<template>
	<div id="courseTable">
  <v-app id="inspire">
    <div>
      <v-toolbar flat color="white">
        <v-toolbar-title>Course</v-toolbar-title>
        <v-divider
          class="mx-2"
          inset
          vertical
        ></v-divider>
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ on }">
            <v-btn color="primary" dark class="mb-2" v-on="on">New Item</v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="headline">{{ formTitle }}</span>
            </v-card-title>
  
            <v-card-text>
              <v-container grid-list-md>
                <v-layout wrap>
                  <v-flex xs12 sm6 md4>
                    <v-text-field v-model="editedItem.courseName" label="Course Name"></v-text-field>
                  </v-flex>
                  <v-flex xs12 sm6 md4>
                    <v-text-field v-model="editedItem.department" label="Department"></v-text-field>
                  </v-flex>
                  <v-flex xs12 sm6 md4>
                    <v-text-field v-model="editedItem.teacherFirstName" label="Teacher First Name"></v-text-field>
                  </v-flex>
                  <v-flex xs12 sm6 md4>
                    <v-text-field v-model="editedItem.teacherLastName" label="Teacher Last Name"></v-text-field>
                  </v-flex>
                 
                </v-layout>
              </v-container>
            </v-card-text>
  
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" flat @click="close">Cancel</v-btn>
              <v-btn color="blue darken-1" flat @click="save">Save</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
      <v-data-table
        :headers="headers"
        :items="courses"
        class="elevation-1"
      >
        <template v-slot:items="props">
          <td>{{ props.item.courseName }}</td>
          <td class="text-xs-left">{{ props.item.department }}</td>
          <td class="text-xs-left">{{ props.item.teacherFirstName }}</td>
          <td class="text-xs-left">{{ props.item.teacherLastName }}</td>
          <td class="justify-center layout px-0">
            <v-icon
              small
              class="mr-2"
              @click="editItem(props.item)"
            >
              edit
            </v-icon>
            <v-icon
              small
              @click="deleteItem(props.item)"
            >
              delete
            </v-icon>
          </td>
        </template>
        <template v-slot:no-data>
          <v-btn color="primary" @click="initialize">Reset</v-btn>
        </template>
      </v-data-table>
    </div>
  </v-app>
</div>
</template>
<script>
export default {
	data: () => ({
    dialog: false,
    headers: [
      {
        text: 'Course Name',
        align: 'left',
        sortable: false,
        value: 'courseName'
      },
      { text: 'Department', value: 'department' },
      { text: 'Teacher First Name', value: 'teacherFirstName' },
      { text: 'Teacher Last Name', value: 'teacherLastName' },
    
    ],
    courses: [],
    editedIndex: -1,
    editedItem: {
      courseName:'',
      SchoolName: "",
      department: '',
      teacherFirstName: '',
      teacherLastName:''
      
    },
    defaultItem: {
      courseName:'',
      department: '',
      teacherFirstName: '',
      teacherLastName:''
    }
  }),

  computed: {
    formTitle () {
      return this.editedIndex === -1 ? 'New Item' : 'Edit Item'
    }
  },

  watch: {
    dialog (val) {
      val || this.close()
    },
    courses(){
      this.$eventBus.$emit("SendCourseTable", this.courses)
    }
  },


  methods: {
    
    editItem (item) {
      this.editedIndex = this.courses.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },

    deleteItem (item) {
      const index = this.courses.indexOf(item)
      confirm('Are you sure you want to delete this item?') && this.courses.splice(index, 1)
    },

    close () {
      this.dialog = false
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      }, 300)
    },

    save () {
      if (this.editedIndex > -1) {
        Object.assign(this.courses[this.editedIndex], this.editedItem)
      } else {
        this.courses.push(this.editedItem)
      }
      this.close()
    }
  }
}
</script>
