<template>
  <div>
    <v-form>
        <v-container>
            <v-layout row wrap>
                <v-flex>
                    <v-text-field
                      label="Solo"
                      placeholder="First Name"
                      solo
                      v-model="firstName"
                    ></v-text-field>
                </v-flex>
                <v-flex>
                    <v-text-field
                      label="Solo"
                      placeholder="Last Name"
                      solo
                      v-model="lastName"
                    ></v-text-field>
                </v-flex>
            </v-layout>
            <v-layout row wrap>
              <v-flex xs12 sm6 md4>
                <v-menu
                  ref="menu"
                  v-model="menu"
                  :close-on-content-click="false"
                  :nudge-right="40"
                  lazy
                  transition="scale-transition"
                  offset-y
                  full-width
                  min-width="290px"
                >
                  <template v-slot:activator="{ on }">
                    <v-text-field
                      v-model="dateOfBirth"
                      label="Birthday date"
                      prepend-icon="event"
                      readonly
                      v-on="on"
                    ></v-text-field>
                  </template>
                  <v-date-picker
                    ref="picker"
                    v-model="dateOfBirth"
                    :max="new Date().toISOString().substr(0, 10)"
                    min="1950-01-01"
                    @change="save"
                  ></v-date-picker>
                </v-menu>
            </v-flex>
          </v-layout>

          <v-btn @click="registerUser" color="primary">Complete Registration</v-btn>
        </v-container>
    </v-form>
  </div>
</template>

<script>
import Axios from 'axios';
//import axios from 'axios'
export default {
  name: "UserRegistration",
  data () {
    return {
      firstName: "",
      lastName: "",
      userName: "",
      email: "",
      dateOfBirth: null,
      menu: false
    }
  },
  watch: {
    menu (val) {
      val && setTimeout(() => (this.$refs.picker.activePicker = 'YEAR'))
    }
  },
  methods: {
    save (date) {
      this.$refs.menu.save(date)
    },

    registerUser() {
        // create a register obj 
        // TODO get token from query if it's there when the page is being generated
        let requestPayload = {
          FirstName: this.firstName,
          LastName: this.lastName,
          DateOfBirth: this.dateOfBirth
        }
        let headersObject = {
          headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + this.$route.query.SITtoken
          }
        }
        // console.log(requestPayload.DateOfBirth)
        let urlRegistration = "http://localhost:59364/api/Registration"
        Axios.post(urlRegistration, requestPayload, headersObject)
             .then(response => {
               console.log(response.data)
               sessionStorage.SITtoken = response.data.SITtoken
               this.$router.push({name: "Home"})
               })
             .catch(error => {console.log(error)})
    }
  },

  created() {
    if (this.$route.query.SITtoken != undefined) {
      sessionStorage.SITtoken = this.$route.query.SITtoken
    }
    else {
      // display error message.
    }
  }
}
</script>