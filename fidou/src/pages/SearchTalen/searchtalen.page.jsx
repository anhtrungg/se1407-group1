import React, { useContext, useEffect, useReducer, useState } from 'react'
import { useSearchParams } from 'react-router-dom'
import { getAuthen, getParam } from '../../axios/authenfunction'
import ProfileMini from '../../components/Profile-mini/ProfileMini.component'
import API from '../../constans/api'
import { AccountContext } from '../../context/AccountProvider'
import './searchtalen.style.scss'

import { yupResolver } from '@hookform/resolvers/yup'
import yup from '../../validator/yup'
import { useForm } from 'react-hook-form'

const queryReducer = (state, action) => {
  switch (action.type) {
    case 'SET_FIELD':
      console.log('dispatch' + action.payload.field)
      state[action.payload.field] = action.payload.value
      return {
        ...state
      }
    default:
      return state
  }
}

//  --- SCHEMA ---
const validationSchema = yup.OBJECT({
  SearchText: yup.FULL_NAME_NOT_REQUIRED,
  minAge: yup.MIN_AGE,
  maxAge: yup.MAX_AGE
})
//  --- END - SCHEMA ---

function SearchTalen() {
  let [categories, setCategories] = useState([])
  let [subCategories, setSubCategories] = useState([])
  let [candidates, setCandidates] = useState([])
  let [isFirstLoad, setIsFirstLoad] = useState(true)
  const [searchParams, setSearchParams] = useSearchParams()
  const accountContext = useContext(AccountContext)
  let { data } = accountContext
  let [query, queryDispatch] = useReducer(queryReducer, {
    category: '',
    subCategory: '',
    gender: '',
    SearchText: '',
    minAge: '',
    maxAge: '',
    accent: ''
  })

  //  --- APPLY SCHEMA TO FORM ---
  const {
    register,
    formState: { errors }
  } = useForm({
    mode: 'onBlur',
    reValidateMode: 'onBlur',
    resolver: yupResolver(validationSchema)
  })
  //  --- END - APPLY SCHEMA TO FORM ---

  useEffect(() => {
    getAuthen(API['GET_SUBCATEGORY']).then(response => {
      setCategories(response.data.data)
    })
    getAuthen(API['GET_CATEGORY']).then(response => {
      setSubCategories(response.data.data)
    })
    // console.log(searchParams.get("SubCategoryId"));
  }, [])

  const search = isButton => {
    let fullQuery = '?search=1'

    // if (isFirstLoad) {

    // }

    // if (query.category != "") {
    fullQuery += `&CategoryId=${query.category}`
    // }

    // if (query.subCategory != "" || searchParams.get("SubCategoryId")) {
    fullQuery += `&SubCategoryId=${query.subCategory}`
    // }

    // if (query.gender != "" || searchParams.get("Gender")) {
    fullQuery += `&Gender=${query.gender}`
    fullQuery += `&SearchText=${query.SearchText}`
    // }
    fullQuery += `&MinAge=${query.minAge}`
    fullQuery += `&MaxAge=${query.maxAge}`
    fullQuery += `&Accent=${query.accent}`
    console.log(fullQuery)

    getParam(API['GET_CANDIDATE_FILTER'], fullQuery).then(response => {
      setCandidates(response.data.data)
    })

    if (isButton) {
      setSearchParams(fullQuery)
    }
    // if (!isFirstLoad) {
    //   setSearchParams(fullQuery);
    // } else {
    //   setIsFirstLoad(false);
    // }
  }

  useEffect(() => {
    if (
      searchParams.get('CategoryId') &&
      query.category != searchParams.get('CategoryId')
    ) {
      console.log('change')
      queryDispatch({
        type: 'SET_FIELD',
        payload: {
          field: 'category',
          value: searchParams.get('CategoryId')
        }
      })
    }

    if (
      searchParams.get('SubCategoryId') &&
      query.subCategory != searchParams.get('SubCategoryId')
    ) {
      queryDispatch({
        type: 'SET_FIELD',
        payload: {
          field: 'subCategory',
          value: searchParams.get('SubCategoryId')
        }
      })
    }

    if (
      searchParams.get('Gender') &&
      query.gender != searchParams.get('Gender')
    ) {
      queryDispatch({
        type: 'SET_FIELD',
        payload: {
          field: 'gender',
          value: searchParams.get('Gender')
        }
      })
    }

    if (
      searchParams.get('MinAge') &&
      query.minAge != searchParams.get('MinAge')
    ) {
      queryDispatch({
        type: 'SET_FIELD',
        payload: {
          field: 'minAge',
          value: searchParams.get('MinAge')
        }
      })
    }

    if (
      searchParams.get('MaxAge') &&
      query.maxAge != searchParams.get('MaxAge')
    ) {
      queryDispatch({
        type: 'SET_FIELD',
        payload: {
          field: 'maxAge',
          value: searchParams.get('MaxAge')
        }
      })
    }

    if (
      searchParams.get('SearchText') &&
      query.SearchText != searchParams.get('SearchText')
    ) {
      queryDispatch({
        type: 'SET_FIELD',
        payload: {
          field: 'SearchText',
          value: searchParams.get('SearchText')
        }
      })
    }
  }, [searchParams])

  useEffect(() => {
    search()
  }, [query])

  return (
    <div className="search-talen">
      <div className="left">
        <div className="item">
          <h3>T???t c??? d???ch v???</h3>
          <div className="list-service">
            <h4
              className={query?.category == '' ? 'active' : ''}
              onClick={() => {
                queryDispatch({
                  type: 'SET_FIELD',
                  payload: {
                    field: 'category',
                    value: ''
                  }
                })
              }}
            >
              T???t c???
            </h4>
            {subCategories.map((item, index) => {
              return (
                <h4
                  className={query?.category == item.id ? 'active' : ''}
                  onClick={() => {
                    queryDispatch({
                      type: 'SET_FIELD',
                      payload: {
                        field: 'category',
                        value: item.id
                      }
                    })
                  }}
                >
                  {item.name}
                </h4>
              )
            })}
          </div>
        </div>
        <div className="item">
          <h3>K?? n??ng</h3>
          <select
            name=""
            id=""
            onChange={e => {
              queryDispatch({
                type: 'SET_FIELD',
                payload: {
                  field: 'subCategory',
                  value: e.target.value
                }
              })
            }}
            value={query.subCategory}
          >
            <option selected value="">
              T???t c???
            </option>
            {categories
              .filter(
                i => i.categoryId == query.category || query.category == ''
              )
              .map((category, index) => {
                return (
                  <option value={category.id} key={index}>
                    {category.name}
                  </option>
                )
              })}
          </select>
        </div>

        <div className="item">
          <h3>T??n ???ng vi??n</h3>
          <input
            {...register('SearchText')}
            className={`${!!errors.SearchText ? 'errorInput' : ''}`}
            type="text"
            value={query.SearchText}
            onChange={e => {
              queryDispatch({
                type: 'SET_FIELD',
                payload: {
                  field: 'SearchText',
                  value: e.target.value
                }
              })
            }}
          />
          <p className="errorMessage">{errors.SearchText?.message}</p>
        </div>

        <div className="item">
          <h3>????? tu???i t??? </h3>
          {/* <input
            type="text"
            value={query.minAge}
            onChange={(e) => {
              queryDispatch({
                type: "SET_FIELD",
                payload: {
                  field: "MinAge",
                  value: e.target.value,
                },
              });
            }}
          />  */}
          <input
            {...register('minAge')}
            className={`${!!errors.minAge ? 'errorInput' : ''}`}
            type="number"
            min="0"
            max="100"
            onChange={e => {
              queryDispatch({
                type: 'SET_FIELD',
                payload: {
                  field: 'minAge',
                  value: e.target.value
                }
              })
            }}
          />
          <p className="errorMessage">{errors.minAge?.message}</p>

          <h3>?????n</h3>
          <input
            {...register('maxAge')}
            className={`${!!errors.maxAge ? 'errorInput' : ''}`}
            type="number"
            min="0"
            max="100"
            onChange={e => {
              queryDispatch({
                type: 'SET_FIELD',
                payload: {
                  field: 'maxAge',
                  value: e.target.value
                }
              })
            }}
          />
          <p className="errorMessage">{errors.maxAge?.message}</p>
        </div>

        <div className="item">
          <h3>Gi???i t??nh</h3>
          <select
            name=""
            id=""
            value={query.gender}
            onChange={e => {
              queryDispatch({
                type: 'SET_FIELD',
                payload: {
                  field: 'gender',
                  value: e.target.value
                }
              })
            }}
          >
            <option selected value="">
              T???t c???
            </option>
            <option value="0">Nam</option>
            <option value="1">N???</option>
          </select>
        </div>

        <div className="item">
          <h3>V??ng mi???n</h3>
          <select
            name=""
            id=""
            value={query.accent}
            onChange={e => {
              queryDispatch({
                type: 'SET_FIELD',
                payload: {
                  field: 'accent',
                  value: e.target.value
                }
              })
            }}
          >
            <option selected value="">
              T???t c???
            </option>
            <option value="0">Mi???n b???c</option>
            <option value="1">Mi???n trung</option>
            <option value="2">Mi???n nam</option>
            <option value="3">Mi???n t??y</option>
          </select>
        </div>
        {/* <div className="item">
          <h3>Tone</h3>
          <select
            name=""
            id=""
            value={query.tone}
            onChange={(e) => {
              queryDispatch({
                type: "SET_FIELD",
                payload: {
                  field: "tone",
                  value: e.target.value,
                },
              });
            }}
          >
            <option selected value>
              T???t c???
            </option>
            <option value="0">Gi???ng tr???m</option>
            <option value="1">Gi???ng v???a</option>
            <option value="2">Gi???ng cao</option>
          </select>
        </div> */}

        {/* <button
          className="button"
          onClick={() => {
            search(true);
          }}
        >
          T??m ki???m
        </button> */}
      </div>
      <div className="right">
        <h2>Danh s??ch ???ng vi??n</h2>
        {candidates.map((item, index) => {
          return (
            <ProfileMini data={item} isShowInvite={data.account?.role == 1} />
          )
        })}
        {!candidates || candidates?.length == 0 ? (
          <p>Kh??ng c?? ???ng c??? vi??n n??o!</p>
        ) : (
          ''
        )}
      </div>
    </div>
  )
}

export default SearchTalen
