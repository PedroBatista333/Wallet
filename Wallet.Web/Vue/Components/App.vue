<template>
    <v-app id="app">
        <v-toolbar class="white" light>
            <v-toolbar-title class="hidden-sm-and-down">Toolbar</v-toolbar-title>
            <v-spacer></v-spacer>

            <v-toolbar-item>
                <v-btn icon dark>
                    <v-icon>add-circle</v-icon>
                </v-btn>
            </v-toolbar-item>
        </v-toolbar>

        <v-container fluid>
            <v-layout row wrap>
                <v-flex xs12 sm5 offset-sm1>
                    <v-layout row wrap>
                        <v-flex xs12>
                            <v-card>
                                <v-card-row>
                                    <div class="balance-result-container">
                                        <div :if="expenseList.balance < 0">
                                            <div class="balance-result balance-result-title red--text darken-1 body-1 text-xs-center">Saldo</div>
                                            <div class="balance-result red--text darken-1 display-1 text-xs-center">R$ {{ expenseList.balance }}</div>
                                        </div>
                                        <div :else>
                                            <div class="balance-result balance-result-title green--text darken-1 body-1 text-xs-center">Saldo</div>
                                            <div class="balance-result green--text darken-1 display-1 text-xs-center">R$ {{ expenseList.balance }}</div>
                                        </div>
                                        <div class="balance-result-details">
                                            <div class="balance-result balance-result-detail blue--text darken-1 body-1 text-xs-left">Receitas: R$ {{ expenseList.income }}</div>
                                            <div class="balance-result red--text darken-1 body-1 text-xs-left">Despesas: R$ {{ expenseList.expense }}</div>
                                        </div>
                                    </div>
                                </v-card-row>
                            </v-card>
                        </v-flex>

                        <v-flex xs12 class="layout-separator">
                            <v-card>
                                <v-card-row>
                                    <v-list two-line class="balance-result">
                                        <div :for="item in expensesList" :key="item">
                                            <v-list-item>
                                                <v-list-tile avatar>
                                                    <v-list-tile-avatar class="headline">
                                                        {{ item.day }}
                                                    </v-list-tile-avatar>
                                                    <v-list-tile-content>
                                                        <v-list-tile-title class="body-2">{{ item.weekname }}</v-list-tile-title>
                                                        <v-list-tile-sub-title class="body-1">{{ item.date }}</v-list-tile-sub-title>
                                                    </v-list-tile-content>
                                                    <v-list-tile-action :if="subitem.value < 0" class="body-1 red--text">
                                                        {{ item.total }}
                                                    </v-list-tile-action>
                                                    <v-list-tile-action :else class="body-1">
                                                        {{ item.total }}
                                                    </v-list-tile-action>
                                                </v-list-tile>
                                            </v-list-item>
                                            <v-list-item>
                                                <v-list>
                                                    <v-list-item  :for="subitem in expensesList.items" :key="subitem">
                                                        <v-list-tile>
                                                            <v-list-tile-content>
                                                                <v-list-tile-title class="body-2">{{ subitem.description }}</v-list-tile-title>
                                                            </v-list-tile-content>
                                                            <v-list-tile-action :if="subitem.value < 0" class="body-1 red--text">
                                                                {{ subitem.value }}
                                                            </v-list-tile-action>
                                                            <v-list-tile-action :else class="body-1">
                                                                {{ subitem.value }}
                                                            </v-list-tile-action>
                                                        </v-list-tile>
                                                    </v-list-item>
                                                </v-list>
                                            </v-list-item>
                                            <v-divider inset></v-divider>
                                        </div>
                                    </v-list>
                                </v-card-row>
                            </v-card>
                        </v-flex>
                    </v-layout>
                </v-flex>

                <v-flex xs12 sm5>
                    <v-layout row wrap>
                        <v-flex xs12>
                            <v-card>
                                <v-card-row>
                                    <chart :options="totalBalance" auto-resize></chart>
                                </v-card-row>
                            </v-card>
                        </v-flex>
                        <v-flex xs12 class="layout-separator">
                            <v-card>
                                <v-card-row>
                                    <chart :options="balanceCategories"></chart>
                                </v-card-row>
                            </v-card>
                        </v-flex>
                    </v-layout>
                </v-flex>
            </v-layout>

        </v-container>
    </v-app>
</template>

<script>
    import { HTTP } from '../Common/http-common'
    import ECharts from 'vue-echarts/components/ECharts.vue'
    import 'echarts/lib/chart/pie'
    import 'echarts/lib/component/tooltip'
    import 'echarts/lib/component/legend'
    import 'echarts/lib/component/title'

    import totalBalance from '../Charts/totalBalance'
    import balanceCategories from '../Charts/balanceCategories'

    export default {
        components: {
            chart: ECharts
        },

        data() {
            return {
                actualYear = new Date().getFullYear(),
                actualMonth = new Date().getMonth() + 1,

                incomes = {},

                totalBalance,
                balanceCategories,
            }
        },

        created() {
            HTTP.get('income/' + this.actualYear + '/' + this.actualMonth)
                .then(response => {
                    this.incomes = response.data
                })
                .catch(e => {
                    this.errors.push(e)
                })
        }
    }
</script>

<style>
    .echarts {
        max-width: 100%;
        height: 300px;
    }

    .balance-result-container {
        width: 100%;
        min-height: 200px;
        padding: 0px 10px 10px 10px;
    }

    .balance-result {
        width: 100%;
    }

    .balance-result-title {
        margin-top: 30px;
    }

    .balance-result-details {
        position: absolute;
        bottom: 10px;
    }

    .balance-result-detail{
        margin-top: 20px;
    }

    .layout-separator {
        margin-top: 15px;
    }
</style>