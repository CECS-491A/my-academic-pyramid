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

          <v-btn color="primary">Primary</v-btn>
        </v-container>
    </v-form>
  </div>
</template>

<script>
import Axios from 'axios';
//import axios from 'axios'
export default {
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
        // create a regieter obj 
        let requestPayload = {
          headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
          'token': this.$router.query.token
          },
          FirstName: firstName,
          LastName: lasteName,
          DateOfBirth: JSON.stringify(dateOfBirth)
        }

        Axios.post("", requestPayload)
             .then(response => {console.log("Registration saved.")})
             .catch(error => {console.log(error)})
    }
  }
}
</script>