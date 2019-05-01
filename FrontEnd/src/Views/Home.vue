<template>
<div class="team">
  <h1>{{info}}</h1>
  <v-container class="my-5">

    <v-lay row wrap>
      <v-flex xs12 sm6 md4 lg3 v-for="person in team" :key="person-name">
        <v-card flat class="text-xs-center ma-3">
          <v-card-text>
            <div class="subheading">{{person.name}}</div>
            <div class="grey--text">{{person.role}}</div>
          </v-card-text>
          <v-card-action>
            <v-btn color="grey">
              <v-icon small left>message</v-icon>
              <span>Message</span>
            </v-btn>
          </v-card-action>
        </v-card>
      </v-flex>
    </v-lay>

  </v-container>
</div>
</template>

<script>
//import axios from 'axios'
export default {
  data () {
    return {
      info: "My Academic Pyramid Homepage",
      team: [
        { name: "Krystal Leon", role: 'Scrum Manager'},
        { name: "Hyunwoo Kim", role: 'Fullstack developer'},
        { name: "Luis Julian", role: 'Fullstack developer'},
        { name: "Arturo Peña Contreras", role: 'Fullstack developer'},
        { name: "Trong Nguyen", role: 'Fullstack developer'},
        { name: "Victor Kim", role: 'Fullstack developer'}
      ]
    }
  }
  
}
</script>
