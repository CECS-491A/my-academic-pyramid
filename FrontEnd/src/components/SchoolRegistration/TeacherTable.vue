<template>
  <div id="teacherTable">
    <v-app id="inspire">
      <div>
        <v-toolbar flat color="white">
          <v-toolbar-title>Teachers</v-toolbar-title>
          <v-divider class="mx-2" inset vertical></v-divider>
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
                      <v-text-field v-model="editedItem.FirstName" label="Teacher FName"></v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md4>
                      <v-text-field v-model="editedItem.MiddleName" label="Teacher MName"></v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md4>
                      <v-text-field v-model="editedItem.LastName" label="Teacher LName"></v-text-field>
                    </v-flex>
                    <v-flex xs12 sm6 md4>
                      <v-text-field v-model="editedItem.DepartmentName" label="Deparment"></v-text-field>
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
        <v-data-table :headers="headers" :items="teachers" class="elevation-1">
          <template v-slot:items="props">
            <td>{{ props.item.FirstName }}</td>
            <td class="text-xs-left">{{ props.item.MiddleName }}</td>
            <td class="text-xs-left">{{ props.item.LastName }}</td>
            <td class="text-xs-left">{{ props.item.DepartmentName }}</td>
            <td class="justify-center layout px-0">
              <v-icon small class="mr-2" @click="editItem(props.item)">edit</v-icon>
              <v-icon small @click="deleteItem(props.item)">delete</v-icon>
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
        text: "Teacher FName",
        align: "left",
        sortable: false,
        value: "FirstName"
      },
      { text: "Teacher Middle Name", value: "MiddleName" },
      { text: "Teacher Last Name", value: "LastName" },
      { text: "Department", value: "DepartmentName" }
    ],
    teachers: [],
    editedIndex: -1,
    editedItem: {
      FirstName: "",
      MiddleName: "",
      LastName: "",
      DepartmentName: ""
    },
    defaultItem: {
      FirstName: "",
      MiddleName: "",
      LastName: "",
      DepartmentName: ""
    }
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "New Item" : "Edit Item";
    }
  },

  watch: {
    dialog(val) {
      val || this.close();
    },
    teachers() {
      this.$eventBus.$emit("SendTeacherTable", this.teachers);
    }
  },

  methods: {
    editItem(item) {
      this.editedIndex = this.teachers.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },

    deleteItem(item) {
      const index = this.teachers.indexOf(item);
      confirm("Are you sure you want to delete this item?") &&
        this.teachers.splice(index, 1);
    },

    close() {
      this.dialog = false;
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      }, 300);
    },

    save() {
      if (this.editedIndex > -1) {
        Object.assign(this.teachers[this.editedIndex], this.editedItem);
      } else {
        this.teachers.push(this.editedItem);
      }
      this.close();
    }
  }
};
</script>
