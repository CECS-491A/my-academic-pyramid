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
          'Authorization': 'Bearer ' + sessionStorage.SITtoken
          }
        }
        // console.log(requestPayload.DateOfBirth)
        let urlRegistration = `${this.$hostname}Registration`
        Axios.post(urlRegistration, requestPayload, headersObject)
             .then(response => {
               sessionStorage.SITtoken = response.data.SITtoken
               this.$router.push({name: "UserHomePage"})
               })
             .catch(error => {console.log(error)})
    }
  },

  created() {        
      console.log("In creation")
      // try to get userinfo
      this.axios.get(`${this.$hostname}UserManager/GetContextId`, 
                     {headers: {'Accept': 'application/json',
                                'Content-Type': 'application/json',
                                'Authorization': `Bearer ${sessionStorage.SITtoken}`}})
                .then(response => {
                    // Get user userid.
                    sessionStorage.SITuserid = response.data.userid
                    sessionStorage.SITtoken = response.data.SITtoken
                })
                .catch(error => {
                  //Indicate an error. Server might be down so just logout the user.
                  // delete token to logout the user.
                  // Redirect user to error page.
                })
  }
}
</script>