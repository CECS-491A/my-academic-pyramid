
<template>
  <v-dialog v-model="dialog" title="Create User">
    <v-app>
      <v-card>
        <v-toolbar>
          <v-toolbar-title>Create User</v-toolbar-title>
        </v-toolbar>
        <v-card-text>
          <form>
            <v-text-field
              id="UserName"
              v-model="formData.UserName"
              :error-messages="emailErrors"
              label="UserName (Email)"
              required
              @input="$v.formData.UserName.$touch()"
              @blur="$v.formData.UserName.$touch()"
            ></v-text-field>

            <v-text-field
              id="FirstName"
              v-model="formData.FirstName"
              :error-messages="firstNameErrors"
              :counter="10"
              label="First Name"
              required
              @input="$v.formData.FirstName.$touch()"
              @blur="$v.formData.FirstName.$touch()"
            ></v-text-field>

            <v-text-field
              id="LastName"
              v-model="formData.LastName"
              :error-messages="lastNameErrors"
              :counter="10"
              label="Last Name"
              required
              @input="$v.formData.LastName.$touch()"
              @blur="$v.formData.LastName.$touch()"
            ></v-text-field>

                  <v-select
        v-model="formData.Catergory"
        :items="catergoryItems"
        :error-messages="catergorySelectErrors"
        label="Catergory"
        required
        @change="$v.formData.Catergory.$touch()"
        @blur="$v.formData.Catergory.$touch()"
      ></v-select>

            <v-menu
              ref="menu"         
              :close-on-content-click="true"
              :nudge-right="40"
              lazy
              transition="scale-transition"
              offset-y
              full-width
              max-width="290px"
              min-width="290px"
            >
              <template v-slot:activator="{ on }">
                <v-text-field
               
                  v-model="formData.DateOfBirth"
                  :close-on-content-click="false"
                  :error-messages="DateOfBirthErrors"
                  label="Date of Birth"
                  prepend-icon="event"
                  readonly
                  v-on="on"
                  @input="$v.formData.DateOfBirth.$touch()"
                  @blur="$v.formData.DateOfBirth.$touch()"

                ></v-text-field>
              </template>
              <v-date-picker 
               ref="picker"
              v-model="formData.DateOfBirth" no-title @input="DateOfBirthBox = false"></v-date-picker>
            </v-menu>
            <v-btn @click="submitData">submit</v-btn>
            <v-alert
              :value="failedAlert"
              type="error"
              transition="scale-transition"
            >Submit Failed. Please check all fields</v-alert>

            <v-alert
              :value="successAlert"
              type="success"
              transition="scale-transition"
            >Submit Sucessfully</v-alert>

            <v-btn @click="clear">clear</v-btn>
          </form>
        </v-card-text>
      </v-card>
    </v-app>
  </v-dialog>
</template>



<script>
/*global console*/ /* eslint no-console: "off" */
import { validationMixin } from "vuelidate";

import { required, maxLength, email } from "vuelidate/lib/validators";

export default {
  name: "CreateUserDiallog",
  mixins: [validationMixin],

  validations: {
    formData: {
      FirstName: { required, maxLength: maxLength(10) },
      LastName: { required, maxLength: maxLength(10) },

      UserName: { required, email },
      DateOfBirth: {required},
      Catergory: {required}

      
    }
  },

  data: () => ({
    formData: {
      UserName: "",
      FirstName: "",
      LastName: "",
      DateOfBirth: new Date().toJSON().substr(0, 10),
      Catergory:null,
    },
    catergoryItems: [
      'Admin',
      'Student',
      'Non-Student'
    ],
    response: "",
    dialog: false,
    submitStatus: null,
    failedAlert: false,
    successAlert: false,
    DateOfBirthBox: false
  }),

  computed: {
    firstNameErrors() {
      const errors = [];

      if (!this.$v.formData.FirstName.$dirty) return errors;

      !this.$v.formData.FirstName.maxLength &&
        errors.push("Name must be at most 10 characters long");

      !this.$v.formData.FirstName.required &&
        errors.push("Last Name is required.");

      return errors;
    },
    lastNameErrors() {
      const errors = [];

      if (!this.$v.formData.LastName.$dirty) return errors;
      !this.$v.formData.LastName.maxLength &&
        errors.push("Name must be at most 10 characters long");

      !this.$v.formData.LastName.required &&
        errors.push("Last Name is required.");

      return errors;
    },

    emailErrors() {
      const errors = [];

      if (!this.$v.formData.UserName.$dirty) return errors;

      !this.$v.formData.UserName.email && errors.push("Must be valid e-mail");

      !this.$v.formData.UserName.required && errors.push("E-mail is required");

      return errors;
    },
    catergorySelectErrors () {
      const errors = []
      if (!this.$v.formData.Catergory.$dirty) return errors
      !this.$v.formData.Catergory.required && errors.push('Item is required')
      return errors
    },

    DateOfBirthErrors(){
      const errors = [];
      if (!this.$v.formData.DateOfBirth.$dirty) return errors;
      !this.$v.formData.DateOfBirth.required && errors.push("Date Of Birth is required");
      return errors;

    }
  },
  created() {
    this.$eventBus.$on("ShowDialog", () => {
      this.dialog = true;
    });
  },
  watch: {
    DateOfBirth (val) {
      val && setTimeout(() => (this.$refs.picker.activePicker = 'YEAR'))
    }
  },
  methods: {
    submitData() {
      console.log("submit");
      this.$v.$touch();
      if (this.$v.$invalid) {
        this.submitStatus = "ERROR";
        this.failedAlert = true;
      } else {
        this.axios({
          method: "POST",
          crossDomain: true,
          url: this.$hostname +"UserManager/",
          data: this.formData,
          headers: {
            "content-type": "application/json",
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Methods":
              "GET, POST, PATCH, PUT, DELETE, OPTIONS"
          }
        }).then(
          result => {
            this.response = result.data;
          },
          error => {
            console.error(error);
          }
        );

        this.submitStatus = "PENDING";
        setTimeout(() => {
          this.successAlert = true;
          this.submitStatus = "OK";
        }, 500);

        this.$eventBus.$emit("UpdateTable");
       
        this.dialog = false;
      }
    },

    clear() {
      this.$v.$reset();

      this.formData.FirstName = "";

      this.formData.LastName = "";

      this.formData.UserName = "";

      this.formData.BirthDate="";

      this.successAlert = false;

      this.failedAlert = false;
      
    }
  }
};
</script>

